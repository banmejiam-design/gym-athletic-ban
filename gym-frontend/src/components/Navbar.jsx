import { Link, useLocation } from 'react-router-dom';

export default function Navbar() {
  const { pathname } = useLocation();

  const links = [
    { to: '/', label: 'Dashboard' },
    { to: '/members', label: 'Miembros' },
    { to: '/trainers', label: 'Entrenadores' },
    { to: '/classes', label: 'Clases' },
    { to: '/memberships', label: 'Membresías' },
    { to: '/enrollments', label: 'Inscripciones' },
  ];

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container">
        <Link className="navbar-brand fw-bold" to="/">
          🏋️ GymManagement
        </Link>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navMenu">
          <span className="navbar-toggler-icon" />
        </button>
        <div className="collapse navbar-collapse" id="navMenu">
          <ul className="navbar-nav ms-auto">
            {links.map(({ to, label }) => (
              <li className="nav-item" key={to}>
                <Link className={`nav-link ${pathname === to ? 'active fw-semibold' : ''}`} to={to}>
                  {label}
                </Link>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </nav>
  );
}
