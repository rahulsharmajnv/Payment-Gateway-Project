document.addEventListener('DOMContentLoaded', () => {
  const urlParams = new URLSearchParams(window.location.search);
  const amount = urlParams.get('amount');
  document.getElementById('amount').value = amount;

  document.getElementById('checkoutForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const form = e.target;
    const data = {
      name: form.name.value,
      email: form.email.value,
      amount: Number(form.amount.value)
    };

    const res = await post('/checkout/initiate-payment', data, true);
    if (res.transactionId) {
      window.location.href = `status.html?txnId=${res.transactionId}`;
    } else {
      alert('Payment initiation failed');
    }
  });
});