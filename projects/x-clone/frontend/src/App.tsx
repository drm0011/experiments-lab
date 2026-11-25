import { FormEvent, useState } from 'react'
import './index.css'

const IDENTITY_URL = import.meta.env.VITE_IDENTITY_URL ?? 'http://localhost:8091'

type RegisterResponse = { id: string; username: string; email: string; token: string }
type Session = { username: string; email: string }

function App() {
  const [username, setUsername] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')
  const [busy, setBusy] = useState(false)
  const [session, setSession] = useState<Session | null>(() => {
    const stored = localStorage.getItem('session')
    return stored ? JSON.parse(stored) : null
  })

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault()
    setError('')
    setBusy(true)
    try {
      const response = await fetch(`${IDENTITY_URL}/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, email, password }),
      })
      if (response.ok) {
        const data: RegisterResponse = await response.json()
        const nextSession: Session = { username: data.username, email: data.email }
        localStorage.setItem('session', JSON.stringify(nextSession))
        localStorage.setItem('token', data.token)
        setSession(nextSession)
        setUsername('')
        setEmail('')
        setPassword('')
      } else {
        const body = await response.json().catch(() => ({}))
        setError(body.error ?? `Request failed (${response.status})`)
      }
    } catch {
      setError('Cannot reach the identity service.')
    } finally {
      setBusy(false)
    }
  }

  const logout = () => {
    localStorage.removeItem('session')
    localStorage.removeItem('token')
    setSession(null)
  }

  if (session) {
    return (
      <main className="container">
        <header className="header">
          <h1>X-Clone</h1>
          <p>Logged in as <strong>{session.username}</strong> ({session.email})</p>
          <button className="secondary" onClick={logout}>Log out</button>
        </header>
        <section className="card">
          <p>Tweets come next (US-3). For now the identity slice is complete.</p>
        </section>
      </main>
    )
  }

  return (
    <main className="container">
      <header className="header">
        <h1>X-Clone</h1>
        <p>Create an account to get started.</p>
      </header>
      <form className="card" onSubmit={handleSubmit}>
        <label>
          Username
          <input
            value={username}
            minLength={3}
            maxLength={32}
            onChange={e => setUsername(e.target.value)}
            required
          />
        </label>
        <label>
          Email
          <input
            type="email"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
          />
        </label>
        <label>
          Password
          <input
            type="password"
            value={password}
            minLength={8}
            onChange={e => setPassword(e.target.value)}
            required
          />
        </label>
        {error && <p className="error">{error}</p>}
        <button type="submit" disabled={busy}>{busy ? 'Registering…' : 'Register'}</button>
      </form>
    </main>
  )
}

export default App
