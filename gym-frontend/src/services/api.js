import axios from 'axios';

const API_BASE = 'http://localhost:5096/api';

const api = axios.create({ baseURL: API_BASE });

export const membersApi = {
  getAll: () => api.get('/members'),
  getById: (id) => api.get(`/members/${id}`),
  create: (data) => api.post('/members', data),
  update: (id, data) => api.put(`/members/${id}`, data),
  delete: (id) => api.delete(`/members/${id}`),
};

export const trainersApi = {
  getAll: () => api.get('/trainers'),
  getById: (id) => api.get(`/trainers/${id}`),
  create: (data) => api.post('/trainers', data),
  update: (id, data) => api.put(`/trainers/${id}`, data),
  delete: (id) => api.delete(`/trainers/${id}`),
};

export const classesApi = {
  getAll: () => api.get('/gymclasses'),
  getById: (id) => api.get(`/gymclasses/${id}`),
  create: (data) => api.post('/gymclasses', data),
  update: (id, data) => api.put(`/gymclasses/${id}`, data),
  delete: (id) => api.delete(`/gymclasses/${id}`),
};

export const enrollmentsApi = {
  getAll: () => api.get('/enrollments'),
  getById: (id) => api.get(`/enrollments/${id}`),
  getByMember: (memberId) => api.get(`/enrollments/member/${memberId}`),
  create: (data) => api.post('/enrollments', data),
  unenroll: (id) => api.patch(`/enrollments/${id}/unenroll`),
};

export const membershipsApi = {
  getAll: () => api.get('/memberships'),
  getById: (id) => api.get(`/memberships/${id}`),
  getByMember: (memberId) => api.get(`/memberships/member/${memberId}`),
  create: (data) => api.post('/memberships', data),
  update: (id, data) => api.put(`/memberships/${id}`, data),
  delete: (id) => api.delete(`/memberships/${id}`),
};
