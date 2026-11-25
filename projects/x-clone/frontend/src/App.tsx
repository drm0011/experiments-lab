import { useState } from 'react'
import RegisterForm from './components/RegisterForm'
import { Session } from './types'

function App() {
  const [session, setSession] = useState<Session | null>(() => {
    const stored = localStorage.getItem('session')
    return stored ? JSON.parse(stored) : null
  })

  const handleRegistered = (nextSession: Session, token: string) => {
    localStorage.setItem('session', JSON.stringify(nextSession))
    localStorage.setItem('token', token)
    setSession(nextSession)
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
      <RegisterForm onRegistered={handleRegistered} />
    </main>
  )
}

export default App
