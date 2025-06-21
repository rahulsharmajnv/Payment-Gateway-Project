const API_BASE = 'http://localhost:5202/api';
async function post(path, data, auth = false) {
  const headers = { 'Content-Type': 'application/json' };
  if (auth) headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
  const res = await fetch(`${API_BASE}${path}`, {
    method: 'POST',
    headers,
    body: JSON.stringify(data)
  });
  return res.json();
};

async function get(path, auth = false) {
  const headers = {};
  if (auth) headers['Authorization'] = `Bearer ${localStorage.getItem('token')}`;
  const res = await fetch(`${API_BASE}${path}`, { headers });
  return res.json();
};