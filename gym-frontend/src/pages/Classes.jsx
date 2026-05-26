import { useState, useEffect } from 'react';
import { classesApi, trainersApi } from '../services/api';

const STATUS_LABELS = { 0: 'Programada', 1: 'En Progreso', 2: 'Completada', 3: 'Cancelada' };
const STATUS_COLORS = { 0: 'primary', 1: 'warning', 2: 'success', 3: 'secondary' };

export default function Classes() {
  const [classes, setClasses] = useState([]);
  const [trainers, setTrainers] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState(null);
  const [form, setForm] = useState({ name: '', description: '', schedule: '', durationMinutes: 60, maxCapacity: 20, trainerId: '', status: 0 });
  const [error, setError] = useState('');

  const load = () => Promise.all([classesApi.getAll(), trainersApi.getAll()])
    .then(([c, t]) => { setClasses(c.data); setTrainers(t.data); }).catch(() => {});

  useEffect(() => { load(); }, []);

  const resetForm = () => { setForm({ name: '', description: '', schedule: '', durationMinutes: 60, maxCapacity: 20, trainerId: '', status: 0 }); setEditing(null); setError(''); };

  const openEdit = (c) => {
    setForm({ name: c.name, description: c.description, schedule: c.schedule.slice(0, 16), durationMinutes: c.durationMinutes, maxCapacity: c.maxCapacity, trainerId: c.trainerId, status: c.status });
    setEditing(c.id); setShowForm(true);
  };

  const handleSubmit = async (e) => {
    e.preventDefault(); setError('');
    const payload = { ...form, trainerId: parseInt(form.trainerId), durationMinutes: parseInt(form.durationMinutes), maxCapacity: parseInt(form.maxCapacity), status: parseInt(form.status) };
    try {
      if (editing) await classesApi.update(editing, payload);
      else await classesApi.create(payload);
      setShowForm(false); resetForm(); load();
    } catch (err) { setError(err.response?.data || 'Error al guardar'); }
  };

  const handleDelete = async (id) => {
    if (!confirm('¿Eliminar esta clase?')) return;
    try { await classesApi.delete(id); load(); }
    catch (err) { alert(err.response?.data || 'Error al eliminar'); }
  };

  return (
    <div className="container py-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Clases <span className="badge bg-warning text-dark">{classes.length}</span></h2>
        <button className="btn btn-warning text-dark" onClick={() => { resetForm(); setShowForm(true); }}>+ Nueva Clase</button>
      </div>

      {showForm && (
        <div className="card mb-4 shadow-sm">
          <div className="card-header"><strong>{editing ? 'Editar' : 'Nueva'} Clase</strong></div>
          <div className="card-body">
            {error && <div className="alert alert-danger py-2">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="row g-3">
                <div className="col-md-6"><label className="form-label">Nombre</label><input className="form-control" value={form.name} onChange={e => setForm(f => ({...f, name: e.target.value}))} required /></div>
                <div className="col-md-6"><label className="form-label">Entrenador</label>
                  <select className="form-select" value={form.trainerId} onChange={e => setForm(f => ({...f, trainerId: e.target.value}))} required>
                    <option value="">Seleccionar...</option>
                    {trainers.map(t => <option key={t.id} value={t.id}>{t.firstName} {t.lastName}</option>)}
                  </select>
                </div>
                <div className="col-12"><label className="form-label">Descripción</label><textarea className="form-control" value={form.description} onChange={e => setForm(f => ({...f, description: e.target.value}))} rows={2} /></div>
                <div className="col-md-4"><label className="form-label">Horario</label><input type="datetime-local" className="form-control" value={form.schedule} onChange={e => setForm(f => ({...f, schedule: e.target.value}))} required /></div>
                <div className="col-md-4"><label className="form-label">Duración (min)</label><input type="number" className="form-control" value={form.durationMinutes} onChange={e => setForm(f => ({...f, durationMinutes: e.target.value}))} min="1" required /></div>
                <div className="col-md-4"><label className="form-label">Capacidad Máx.</label><input type="number" className="form-control" value={form.maxCapacity} onChange={e => setForm(f => ({...f, maxCapacity: e.target.value}))} min="1" required /></div>
                {editing && <div className="col-md-4"><label className="form-label">Estado</label>
                  <select className="form-select" value={form.status} onChange={e => setForm(f => ({...f, status: e.target.value}))}>
                    {Object.entries(STATUS_LABELS).map(([k, v]) => <option key={k} value={k}>{v}</option>)}
                  </select>
                </div>}
              </div>
              <div className="mt-3 d-flex gap-2">
                <button type="submit" className="btn btn-warning text-dark">Guardar</button>
                <button type="button" className="btn btn-secondary" onClick={() => { setShowForm(false); resetForm(); }}>Cancelar</button>
              </div>
            </form>
          </div>
        </div>
      )}

      <div className="table-responsive">
        <table className="table table-hover align-middle">
          <thead className="table-dark">
            <tr><th>#</th><th>Clase</th><th>Entrenador</th><th>Horario</th><th>Duración</th><th>Capacidad</th><th>Estado</th><th>Acciones</th></tr>
          </thead>
          <tbody>
            {classes.length === 0 && <tr><td colSpan={8} className="text-center text-muted">Sin clases</td></tr>}
            {classes.map(c => (
              <tr key={c.id}>
                <td>{c.id}</td>
                <td><strong>{c.name}</strong></td>
                <td>{c.trainerName}</td>
                <td>{new Date(c.schedule).toLocaleString('es-CO')}</td>
                <td>{c.durationMinutes} min</td>
                <td>{c.maxCapacity}</td>
                <td><span className={`badge bg-${STATUS_COLORS[c.status]}`}>{STATUS_LABELS[c.status]}</span></td>
                <td>
                  <button className="btn btn-sm btn-outline-warning me-1" onClick={() => openEdit(c)}>Editar</button>
                  <button className="btn btn-sm btn-outline-danger" onClick={() => handleDelete(c.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
