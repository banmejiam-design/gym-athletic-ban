import { useState, useEffect } from 'react';
import { trainersApi } from '../services/api';

export default function Trainers() {
  const [trainers, setTrainers] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState(null);
  const [form, setForm] = useState({ firstName: '', lastName: '', email: '', phone: '', specialization: '' });
  const [error, setError] = useState('');

  const load = () => trainersApi.getAll().then(r => setTrainers(r.data)).catch(() => {});
  useEffect(() => { load(); }, []);

  const resetForm = () => { setForm({ firstName: '', lastName: '', email: '', phone: '', specialization: '' }); setEditing(null); setError(''); };

  const openEdit = (t) => {
    setForm({ firstName: t.firstName, lastName: t.lastName, email: t.email, phone: t.phone, specialization: t.specialization });
    setEditing(t.id); setShowForm(true);
  };

  const handleSubmit = async (e) => {
    e.preventDefault(); setError('');
    try {
      if (editing) await trainersApi.update(editing, form);
      else await trainersApi.create(form);
      setShowForm(false); resetForm(); load();
    } catch (err) { setError(err.response?.data || 'Error al guardar'); }
  };

  const handleDelete = async (id) => {
    if (!confirm('¿Eliminar este entrenador?')) return;
    try { await trainersApi.delete(id); load(); }
    catch (err) { alert(err.response?.data || 'Error al eliminar'); }
  };

  return (
    <div className="container py-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Entrenadores <span className="badge bg-success">{trainers.length}</span></h2>
        <button className="btn btn-success" onClick={() => { resetForm(); setShowForm(true); }}>+ Nuevo Entrenador</button>
      </div>

      {showForm && (
        <div className="card mb-4 shadow-sm">
          <div className="card-header"><strong>{editing ? 'Editar' : 'Nuevo'} Entrenador</strong></div>
          <div className="card-body">
            {error && <div className="alert alert-danger py-2">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="row g-3">
                <div className="col-md-6"><label className="form-label">Nombre</label><input className="form-control" value={form.firstName} onChange={e => setForm(f => ({...f, firstName: e.target.value}))} required /></div>
                <div className="col-md-6"><label className="form-label">Apellido</label><input className="form-control" value={form.lastName} onChange={e => setForm(f => ({...f, lastName: e.target.value}))} required /></div>
                <div className="col-md-6"><label className="form-label">Email</label><input type="email" className="form-control" value={form.email} onChange={e => setForm(f => ({...f, email: e.target.value}))} required /></div>
                <div className="col-md-6"><label className="form-label">Teléfono</label><input className="form-control" value={form.phone} onChange={e => setForm(f => ({...f, phone: e.target.value}))} /></div>
                <div className="col-12"><label className="form-label">Especialización</label><input className="form-control" value={form.specialization} onChange={e => setForm(f => ({...f, specialization: e.target.value}))} required /></div>
              </div>
              <div className="mt-3 d-flex gap-2">
                <button type="submit" className="btn btn-success">Guardar</button>
                <button type="button" className="btn btn-secondary" onClick={() => { setShowForm(false); resetForm(); }}>Cancelar</button>
              </div>
            </form>
          </div>
        </div>
      )}

      <div className="row g-3">
        {trainers.length === 0 && <p className="text-muted">Sin entrenadores registrados.</p>}
        {trainers.map(t => (
          <div className="col-md-4" key={t.id}>
            <div className="card shadow-sm h-100">
              <div className="card-body">
                <h5 className="card-title">🏋️ {t.firstName} {t.lastName}</h5>
                <p className="card-text mb-1"><strong>Especialización:</strong> {t.specialization}</p>
                <p className="card-text mb-1"><small className="text-muted">{t.email}</small></p>
                <p className="card-text"><small className="text-muted">{t.phone}</small></p>
              </div>
              <div className="card-footer d-flex gap-2">
                <button className="btn btn-sm btn-outline-warning" onClick={() => openEdit(t)}>Editar</button>
                <button className="btn btn-sm btn-outline-danger" onClick={() => handleDelete(t.id)}>Eliminar</button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
