import { useState, useEffect } from 'react';
import { enrollmentsApi, membersApi, classesApi } from '../services/api';

export default function Enrollments() {
  const [enrollments, setEnrollments] = useState([]);
  const [members, setMembers] = useState([]);
  const [classes, setClasses] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [form, setForm] = useState({ memberId: '', gymClassId: '' });
  const [error, setError] = useState('');

  const load = () => Promise.all([enrollmentsApi.getAll(), membersApi.getAll(), classesApi.getAll()])
    .then(([e, m, c]) => { setEnrollments(e.data); setMembers(m.data); setClasses(c.data); }).catch(() => {});

  useEffect(() => { load(); }, []);

  const handleSubmit = async (e) => {
    e.preventDefault(); setError('');
    try {
      await enrollmentsApi.create({ memberId: parseInt(form.memberId), gymClassId: parseInt(form.gymClassId) });
      setShowForm(false); setForm({ memberId: '', gymClassId: '' }); load();
    } catch (err) { setError(err.response?.data || 'Error al inscribir'); }
  };

  const handleUnenroll = async (id) => {
    if (!confirm('¿Cancelar esta inscripción?')) return;
    try { await enrollmentsApi.unenroll(id); load(); }
    catch (err) { alert(err.response?.data || 'Error'); }
  };

  return (
    <div className="container py-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Inscripciones <span className="badge bg-info text-dark">{enrollments.filter(e => e.isActive).length} activas</span></h2>
        <button className="btn btn-info text-dark" onClick={() => { setForm({ memberId: '', gymClassId: '' }); setError(''); setShowForm(true); }}>+ Inscribir Miembro</button>
      </div>

      {showForm && (
        <div className="card mb-4 shadow-sm">
          <div className="card-header"><strong>Nueva Inscripción</strong></div>
          <div className="card-body">
            {error && <div className="alert alert-danger py-2">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="row g-3">
                <div className="col-md-6"><label className="form-label">Miembro</label>
                  <select className="form-select" value={form.memberId} onChange={e => setForm(f => ({...f, memberId: e.target.value}))} required>
                    <option value="">Seleccionar miembro...</option>
                    {members.map(m => <option key={m.id} value={m.id}>{m.firstName} {m.lastName}</option>)}
                  </select>
                </div>
                <div className="col-md-6"><label className="form-label">Clase</label>
                  <select className="form-select" value={form.gymClassId} onChange={e => setForm(f => ({...f, gymClassId: e.target.value}))} required>
                    <option value="">Seleccionar clase...</option>
                    {classes.map(c => <option key={c.id} value={c.id}>{c.name} - {c.trainerName}</option>)}
                  </select>
                </div>
              </div>
              <div className="mt-3 d-flex gap-2">
                <button type="submit" className="btn btn-info text-dark">Inscribir</button>
                <button type="button" className="btn btn-secondary" onClick={() => setShowForm(false)}>Cancelar</button>
              </div>
            </form>
          </div>
        </div>
      )}

      <div className="table-responsive">
        <table className="table table-hover align-middle">
          <thead className="table-dark">
            <tr><th>#</th><th>Miembro</th><th>Clase</th><th>Fecha Inscripción</th><th>Estado</th><th>Acciones</th></tr>
          </thead>
          <tbody>
            {enrollments.length === 0 && <tr><td colSpan={6} className="text-center text-muted">Sin inscripciones</td></tr>}
            {enrollments.map(e => (
              <tr key={e.id}>
                <td>{e.id}</td>
                <td>{e.memberName}</td>
                <td>{e.className}</td>
                <td>{new Date(e.enrollmentDate).toLocaleDateString('es-CO')}</td>
                <td><span className={`badge bg-${e.isActive ? 'success' : 'secondary'}`}>{e.isActive ? 'Activa' : 'Cancelada'}</span></td>
                <td>
                  {e.isActive && <button className="btn btn-sm btn-outline-danger" onClick={() => handleUnenroll(e.id)}>Cancelar</button>}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
