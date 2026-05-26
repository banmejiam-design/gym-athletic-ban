import { useState, useEffect } from 'react';
import { membershipsApi, membersApi } from '../services/api';

const TYPE_LABELS = { 0: 'Mensual', 1: 'Trimestral', 2: 'Anual' };
const STATUS_LABELS = { 0: 'Pendiente', 1: 'Activa', 2: 'Vencida', 3: 'Cancelada' };
const STATUS_COLORS = { 0: 'secondary', 1: 'success', 2: 'danger', 3: 'dark' };

export default function Memberships() {
  const [memberships, setMemberships] = useState([]);
  const [members, setMembers] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [form, setForm] = useState({ memberId: '', type: 0, startDate: '', endDate: '', price: '' });
  const [error, setError] = useState('');

  const load = () => Promise.all([membershipsApi.getAll(), membersApi.getAll()])
    .then(([ms, m]) => { setMemberships(ms.data); setMembers(m.data); }).catch(() => {});

  useEffect(() => { load(); }, []);

  const resetForm = () => { setForm({ memberId: '', type: 0, startDate: '', endDate: '', price: '' }); setError(''); };

  const handleSubmit = async (e) => {
    e.preventDefault(); setError('');
    const payload = { memberId: parseInt(form.memberId), type: parseInt(form.type), startDate: form.startDate, endDate: form.endDate, price: parseFloat(form.price) };
    try { await membershipsApi.create(payload); setShowForm(false); resetForm(); load(); }
    catch (err) { setError(err.response?.data || 'Error al guardar'); }
  };

  const handleDelete = async (id) => {
    if (!confirm('¿Eliminar esta membresía?')) return;
    try { await membershipsApi.delete(id); load(); }
    catch (err) { alert(err.response?.data || 'Error al eliminar'); }
  };

  return (
    <div className="container py-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Membresías <span className="badge bg-secondary">{memberships.length}</span></h2>
        <button className="btn btn-secondary" onClick={() => { resetForm(); setShowForm(true); }}>+ Nueva Membresía</button>
      </div>

      {showForm && (
        <div className="card mb-4 shadow-sm">
          <div className="card-header"><strong>Nueva Membresía</strong></div>
          <div className="card-body">
            {error && <div className="alert alert-danger py-2">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="row g-3">
                <div className="col-md-6"><label className="form-label">Miembro</label>
                  <select className="form-select" value={form.memberId} onChange={e => setForm(f => ({...f, memberId: e.target.value}))} required>
                    <option value="">Seleccionar...</option>
                    {members.map(m => <option key={m.id} value={m.id}>{m.firstName} {m.lastName}</option>)}
                  </select>
                </div>
                <div className="col-md-6"><label className="form-label">Tipo</label>
                  <select className="form-select" value={form.type} onChange={e => setForm(f => ({...f, type: e.target.value}))}>
                    {Object.entries(TYPE_LABELS).map(([k, v]) => <option key={k} value={k}>{v}</option>)}
                  </select>
                </div>
                <div className="col-md-4"><label className="form-label">Fecha Inicio</label><input type="date" className="form-control" value={form.startDate} onChange={e => setForm(f => ({...f, startDate: e.target.value}))} required /></div>
                <div className="col-md-4"><label className="form-label">Fecha Fin</label><input type="date" className="form-control" value={form.endDate} onChange={e => setForm(f => ({...f, endDate: e.target.value}))} required /></div>
                <div className="col-md-4"><label className="form-label">Precio (COP)</label><input type="number" className="form-control" value={form.price} onChange={e => setForm(f => ({...f, price: e.target.value}))} min="0" step="1000" required /></div>
              </div>
              <div className="mt-3 d-flex gap-2">
                <button type="submit" className="btn btn-secondary">Guardar</button>
                <button type="button" className="btn btn-outline-secondary" onClick={() => { setShowForm(false); resetForm(); }}>Cancelar</button>
              </div>
            </form>
          </div>
        </div>
      )}

      <div className="table-responsive">
        <table className="table table-hover align-middle">
          <thead className="table-dark">
            <tr><th>#</th><th>Miembro</th><th>Tipo</th><th>Estado</th><th>Inicio</th><th>Fin</th><th>Precio</th><th>Acciones</th></tr>
          </thead>
          <tbody>
            {memberships.length === 0 && <tr><td colSpan={8} className="text-center text-muted">Sin membresías</td></tr>}
            {memberships.map(ms => (
              <tr key={ms.id}>
                <td>{ms.id}</td>
                <td>{ms.memberName}</td>
                <td>{TYPE_LABELS[ms.type]}</td>
                <td><span className={`badge bg-${STATUS_COLORS[ms.status]}`}>{STATUS_LABELS[ms.status]}</span></td>
                <td>{new Date(ms.startDate).toLocaleDateString('es-CO')}</td>
                <td>{new Date(ms.endDate).toLocaleDateString('es-CO')}</td>
                <td>${ms.price.toLocaleString('es-CO')}</td>
                <td><button className="btn btn-sm btn-outline-danger" onClick={() => handleDelete(ms.id)}>Eliminar</button></td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
