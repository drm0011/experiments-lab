import { useEffect, useState } from 'react'

const CATALOG_URL = import.meta.env.VITE_CATALOG_URL ?? 'http://localhost:8081'
const ORDERS_URL = import.meta.env.VITE_ORDERS_URL ?? 'http://localhost:8082'

type Item = { id: number; name: string; price: number }
type Order = { id: number; itemId: number; quantity: number }

function App() {
  const [items, setItems] = useState<Item[]>([])
  const [orders, setOrders] = useState<Order[]>([])

  useEffect(() => {
    fetch(`${CATALOG_URL}/items`)
      .then(r => r.json())
      .then(setItems)
      .catch(() => setItems([]))
    fetch(`${ORDERS_URL}/orders`)
      .then(r => r.json())
      .then(setOrders)
      .catch(() => setOrders([]))
  }, [])

  return (
    <main>
      <h1>Microservices Playground</h1>
      <section>
        <h2>Catalog</h2>
        <ul>
          {items.map(i => (
            <li key={i.id}>{i.name} - {i.price}</li>
          ))}
        </ul>
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
