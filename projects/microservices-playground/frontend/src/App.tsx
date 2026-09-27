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
  const [itemId, setItemId] = useState(1)
  const [quantity, setQuantity] = useState(1)
  const [message, setMessage] = useState('')

  const refresh = () => {
    fetch(`${CATALOG_URL}/items`)
      .then(r => r.json())
      .then(setItems)
      .catch(() => setItems([]))
    fetch(`${INVENTORY_URL}/stock`)
      .then(r => r.json())
      .then(setStock)
      .catch(() => setStock([]))
    fetch(`${ORDERS_URL}/orders`)
      .then(r => r.json())
      .then(setOrders)
      .catch(() => setOrders([]))
  }

  useEffect(refresh, [])

  const placeOrder = async () => {
    const res = await fetch(`${ORDERS_URL}/orders`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ itemId, quantity })
    })
    setMessage(res.ok ? 'Order placed' : `Order failed (${res.status})`)
    refresh()
  }

  const stockFor = (id: number) => stock.find(s => s.itemId === id)?.quantity ?? '?'

  return (
    <main>
      <h1>Microservices Playground — Tiny Shop</h1>
      <section>
        <h2>Catalog</h2>
        <ul>
          {items.map(i => (
            <li key={i.id}>{i.name} - {i.price} (stock: {stockFor(i.id)})</li>
          ))}
        </ul>
      </section>
      <section>
        <h2>Place order</h2>
        <select value={itemId} onChange={e => setItemId(Number(e.target.value))}>
          {items.map(i => (
            <option key={i.id} value={i.id}>{i.name}</option>
          ))}
        </select>
        <input type="number" min={1} value={quantity} onChange={e => setQuantity(Number(e.target.value))} />
        <button onClick={placeOrder}>Order</button>
        <p>{message}</p>
      </section>
      <section>
        <h2>Orders</h2>
        <ul>
          {orders.map(o => (
            <li key={o.id}>#{o.id}: item {o.itemId} x{o.quantity}</li>
          ))}
        </ul>
      </section>
    </main>
  )
}

export default App
