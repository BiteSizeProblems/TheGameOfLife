function showCustomModal(title, message) {
    document.getElementById('customModalTitle').textContent = title;
    document.getElementById('customModalMessage').textContent = message;

    $('#customModal').modal('show');
}