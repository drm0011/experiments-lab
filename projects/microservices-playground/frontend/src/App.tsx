import { useEffect, useState } from 'react'

const CATALOG_URL = import.meta.env.VITE_CATALOG_URL ?? 'http://localhost:8081'
const ORDERS_URL = import.meta.env.VITE_ORDERS_URL ?? 'http://localhost:8082'
const INVENTORY_URL = import.meta.env.VITE_INVENTORY_URL ?? 'http://localhost:8083'

type Item = { id: number; name: string; price: number }
type Order = { id: number; itemId: number; quantity: number }
type StockEntry = { itemId: number; quantity: number }

function App() {
  const [items, setItems] = useState<Item[]>([])
  const [stock, setStock] = useState<StockEntry[]>([])
  const [orders, setOrders] = useState<Order[]>([])
  const [toast, setToast] = useState<{ kind: 'ok' | 'error'; text: string } | null>(null)
  const [loading, setLoading] = useState(true)

  const load = async () => {
    const [i, s, o] = await Promise.all([
      fetch(`${CATALOG_URL}/items`).then(r => r.json()).catch(() => []),
      fetch(`${INVENTORY_URL}/stock`).then(r => r.json()).catch(() => []),
      fetch(`${ORDERS_URL}/orders`).then(r => r.json()).catch(() => []),
    ])
    setItems(i)
    setStock(s)
    setOrders(o)
    setLoading(false)
  }

  useEffect(() => {
    load()
  }, [])

  const showToast = (kind: 'ok' | 'error', text: string) => {
    setToast({ kind, text })
    setTimeout(() => setToast(null), 3000)
  }

  const placeOrder = async (itemId: number, quantity: number) => {
    const res = await fetch(`${ORDERS_URL}/orders`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ itemId, quantity }),
    })
    if (res.ok) {
      showToast('ok', 'Order placed')
    } else {
      const body = await res.json().catch(() => ({}))
      showToast('error', `Order failed (${res.status})${body.error ? `: ${body.error}` : ''}`)
    }
    load()
  }

  const stockFor = (id: number) => stock.find(s => s.itemId === id)?.quantity
  const itemName = (id: number) => items.find(i => i.id === id)?.name ?? `item ${id}`

  if (loading) {
    return <div className="container">Loading shop…</div>
  }

  return (
    <div className="container">
      <header className="header">
        <h1>Microservices Shop</h1>
        <p>Catalog · Inventory · Orders — one service each</p>
      </header>

      <section>
        <h2 className="section-title">Products</h2>
        <div className="grid">
          {items.map(item => (
            <ProductCard
              key={item.id}
              item={item}
              stock={stockFor(item.id)}
              onOrder={placeOrder}
            />
          ))}
        </div>
      </section>

      <section>
        <h2 className="section-title">Orders</h2>
        {orders.length === 0 ? (
          <p className="muted">No orders yet — place one above.</p>
        ) : (
          <ul className="order-list">
            {orders.map(o => (
              <li key={o.id}>
                <span className="order-id">#{o.id}</span>
                <span>{itemName(o.itemId)}</span>
                <span className="muted">×{o.quantity}</span>
              </li>
            ))}
          </ul>
        )}
      </section>

      {toast && <div className={`toast toast-${toast.kind}`}>{toast.text}</div>}
    </div>
  )
}

function ProductCard({ item, stock, onOrder }: {
  item: Item
  stock: number | undefined
  onOrder: (itemId: number, quantity: number) => void
}) {
  const [quantity, setQuantity] = useState(1)

  const badge = stock === undefined
    ? <span className="badge badge-unknown">stock unknown</span>
    : stock === 0
      ? <span className="badge badge-out">out of stock</span>
      : stock < 5
        ? <span className="badge badge-low">only {stock} left</span>
        : <span className="badge badge-in">in stock ({stock})</span>

  return (
    <article className="card">
      <div className="card-head">
        <h3>{item.name}</h3>
        <div className="price">€{item.price.toFixed(2)}</div>
      </div>
      {badge}
      <div className="card-actions">
        <input
          type="number"
          min={1}
          value={quantity}
          onChange={e => setQuantity(Math.max(1, Number(e.target.value)))}
        />
        <button
          disabled={stock === 0 || stock === undefined}
          onClick={() => onOrder(item.id, quantity)}
        >
          Order
        </button>
      </div>
    </article>
  )
}

export default App
