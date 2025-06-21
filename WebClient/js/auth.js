document.addEventListener('DOMContentLoaded', () => {
  const loginForm = document.getElementById('loginForm');
  const registerForm = document.getElementById('registerForm');

  if (loginForm) {
    loginForm.addEventListener('submit', async (e) => {
      e.preventDefault();
      const res = await post('/auth/login', {
        email: e.target.email.value,
        password: e.target.password.value
      });
      if (res.token) {
        localStorage.setItem('token', res.token);
        alert('Login successful');
        location.href = 'account.html';
      } else alert('Login failed');
    });
  }

  if (registerForm) {
    registerForm.addEventListener('submit', async (e) => {
      e.preventDefault();
      const res = await post('/auth/register', {
        name: e.target.name.value,
        email: e.target.email.value,
        password: e.target.password.value
      });
      if (res.success) {
        alert('Registration successful');
        location.href = 'login.html';
      } else alert('Registration failed');
    });
  }
});