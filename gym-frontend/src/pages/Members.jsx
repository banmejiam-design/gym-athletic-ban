import { useState, useEffect } from 'react';
import { membersApi } from '../services/api';

export default function Members() {
  const [members, setMembers] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState(null);
  const [form, setForm] = useState({ firstName: '', lastName: '', email: '', phone: '', dateOfBirth: '' });
  const [error, setError] = useState('');

  const load = () => membersApi.getAll().then(r => setMembers(r.data)).catch(() => {});

  useEffect(() => { load(); }, []);

  const resetForm = () => { setForm({ firstName: '', lastName: '', email: '', phone: '', dateOfBirth: '' }); setEditing(null); setError(''); };

  const openCreate = () => { resetForm(); setShowForm(true); };
  const openEdit = (m) => {
    setForm({ firstName: m.firstName, lastName: m.lastName, email: m.email, phone: m.phone, dateOfBirth: m.dateOfBirth.split('T')[0] });
    setEditing(m.id);
    setShowForm(true);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    try {
      if (editing) await membersApi.update(editing, form);
      else await membersApi.create(form);
      setShowForm(false); resetForm(); load();
    } catch (err) {
      setError(err.response?.data || 'Error al guardar');
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('¿Eliminar este miembro?')) return;
    try { await membersApi.delete(id); load(); }
    catch (err) { alert(err.response?.data || 'Error al eliminar'); }
  };

  return (
    <div className="container py-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Miembros <span className="badge bg-primary">{members.length}</span></h2>
        <button className="btn btn-primary" onClick={openCreate}>+ Nuevo Miembro</button>
      </div>

      {showForm && (
        <div className="card mb-4 shadow-sm">
          <div className="card-header"><strong>{editing ? 'Editar' : 'Nuevo'} Miembro</strong></div>
          <div className="card-body">
            {error && <div className="alert alert-danger py-2">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="row g-3">
                <div className="col-md-6"><label className="form-label">Nombre</label><input className="form-control" value={form.firstName} onChange={e => setForm(f => ({...f, firstName: e.target.value}))} required /></div>
                <div className="col-md-6"><label className="form-label">Apellido</label><input className="form-control" value={form.lastName} onChange={e => setForm(f => ({...f, lastName: e.target.value}))} required /></div>
                <div className="col-md-6"><label className="form-label">Email</label><input type="email" className="form-control" value={form.email} onChange={e => setForm(f => ({...f, email: e.target.value}))} required /></div>
                <div className="col-md-6"><label className="form-label">Teléfono</label><input className="form-control" value={form.phone} onChange={e => setForm(f => ({...f, phone: e.target.value}))} /></div>
                <div className="col-md-6"><label className="form-label">Fecha de Nacimiento</label><input type="date" className="form-control" value={form.dateOfBirth} onChange={e => setForm(f => ({...f, dateOfBirth: e.target.value}))} required /></div>
              </div>
              <div className="mt-3 d-flex gap-2">
                <button type="submit" className="btn btn-success">Guardar</button>
                <button type="button" className="btn btn-secondary" onClick={() => { setShowForm(false); resetForm(); }}>Cancelar</button>
              </div>
            </form>
          </div>
        </div>
      )}

      <div className="table-responsive">
        <table className="table table-hover align-middle">
          <thead className="table-dark">
            <tr><th>#</th><th>Nombre</th><th>Email</th><th>Teléfono</th><th>Registro</th><th>Acciones</th></tr>
          </thead>
          <tbody>
            {members.length === 0 && <tr><td colSpan={6} className="text-center text-muted">Sin miembros</td></tr>}
            {members.map(m => (
              <tr key={m.id}>
                <td>{m.id}</td>
                <td><strong>{m.firstName} {m.lastName}</strong></td>
                <td>{m.email}</td>
                <td>{m.phone}</td>
                <td>{new Date(m.registrationDate).toLocaleDateString('es-CO')}</td>
                <td>
                  <button className="btn btn-sm btn-outline-warning me-1" onClick={() => openEdit(m)}>Editar</button>
                  <button className="btn btn-sm btn-outline-danger" onClick={() => handleDelete(m.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
