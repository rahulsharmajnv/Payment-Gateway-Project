const statusDiv = document.getElementById('status');
const urlParams = new URLSearchParams(window.location.search);
const txnId = urlParams.get('txnId');

async function checkStatus() {
  const res = await get(`/payment/payment-status/${txnId}`, true);
  statusDiv.textContent = `Status: ${res.status}`;
   location.href = 'account.html';
  if (res.status === 'Pending') {
    setTimeout(checkStatus, 3000);
  }
}

checkStatus();