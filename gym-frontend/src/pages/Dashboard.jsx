import { useState, useEffect } from 'react';
import { membersApi, trainersApi, classesApi, enrollmentsApi } from '../services/api';
import { Link } from 'react-router-dom';

export default function Dashboard() {
  const [stats, setStats] = useState({ members: 0, trainers: 0, classes: 0, enrollments: 0 });

  useEffect(() => {
    Promise.all([
      membersApi.getAll(),
      trainersApi.getAll(),
      classesApi.getAll(),
      enrollmentsApi.getAll(),
    ]).then(([m, t, c, e]) => {
      setStats({
        members: m.data.length,
        trainers: t.data.length,
        classes: c.data.length,
        enrollments: e.data.length,
      });
    }).catch(() => {});
  }, []);

  const cards = [
    { label: 'Miembros', value: stats.members, icon: '👥', color: 'primary', to: '/members' },
    { label: 'Entrenadores', value: stats.trainers, icon: '🏋️', color: 'success', to: '/trainers' },
    { label: 'Clases', value: stats.classes, icon: '📋', color: 'warning', to: '/classes' },
    { label: 'Inscripciones', value: stats.enrollments, icon: '✅', color: 'info', to: '/enrollments' },
  ];

  return (
    <div className="container py-4">
      <h1 className="mb-1">Sistema de Gestión de Gimnasio</h1>
      <p className="text-muted mb-4">Administra miembros, entrenadores, clases y membresías</p>
      <div className="row g-4">
        {cards.map(({ label, value, icon, color, to }) => (
          <div className="col-sm-6 col-lg-3" key={label}>
            <Link to={to} className="text-decoration-none">
              <div className={`card border-${color} shadow-sm h-100`}>
                <div className="card-body text-center">
                  <div style={{ fontSize: '2.5rem' }}>{icon}</div>
                  <h2 className={`text-${color} fw-bold mb-0`}>{value}</h2>
                  <p className="text-muted mb-0">{label}</p>
                </div>
              </div>
            </Link>
          </div>
        ))}
      </div>

      <div className="mt-5">
        <h4>Acciones rápidas</h4>
        <div className="d-flex gap-2 flex-wrap">
          <Link to="/members" className="btn btn-primary">+ Nuevo Miembro</Link>
          <Link to="/trainers" className="btn btn-success">+ Nuevo Entrenador</Link>
          <Link to="/classes" className="btn btn-warning text-dark">+ Nueva Clase</Link>
          <Link to="/memberships" className="btn btn-secondary">+ Nueva Membresía</Link>
        </div>
      </div>
    </div>
  );
}
