
function loadSavedCardModal() {
    fetch("/Payment/GetSavedCards")
        .then(response => response.json())
        .then(savedCards => {
            let savedCardsHtml = '<h5>Kayıtlı Kartlarınız</h5>';
            if (savedCards.length === 0) {
                savedCardsHtml += '<p>Kayıtlı kart bulunmamaktadır.</p>';
            } else {
                savedCards.forEach(card => {
                    // Tarih formatını düzenle
                    let formattedExpiryDate;
                    if (card.expiryDate.includes('-')) {
                        const [year, month] = card.expiryDate.split('-');
                        formattedExpiryDate = `${month}/${year.slice(-2)}`; // Burada backtick (\``) işareti kullanmalısınız
                    } else {
                        formattedExpiryDate = card.expiryDate;
                    }

                    savedCardsHtml += `
                                <div class="card mb-3">
                                    <div class="card-body">
                                        <div>Kart Numarası: ${card.cardNumber.replace(/\d(?=\d{4})/g, "*")}</div>
                                        <div>Kart Sahibi: ${card.cardHolderName}</div>
                                        <div>Son Kullanma Tarihi: ${formattedExpiryDate}</div>
                                        <button class="btn btn-primary" onclick="selectSavedCard(${card.savedCardId}, '${card.cardNumber}', '${card.cardHolderName}', '${card.expiryDate}')">Bu Kartla Ödeme Yap</button>
                                    </div>
                                </div>
                            `;
                });
            }
            document.getElementById('savedCardsList').innerHTML = savedCardsHtml;
            var modal = new bootstrap.Modal(document.getElementById('savedCardModal'));
            modal.show();
        })
        .catch(error => {
            console.error('Kayıtlı kartlar yüklenirken hata oluştu:', error);
        });
}
function selectSavedCard(savedCardId, cardNumber, cardHolderName, expiryDate) {
    // Form alanlarını temizle ve doldur
    const form = document.querySelector('form');

    // Yeni bir hidden input ekle (eğer yoksa)
    let hiddenInput = document.getElementById('selectedCardId');
    if (!hiddenInput) {
        hiddenInput = document.createElement('input');
        hiddenInput.type = 'hidden';
        hiddenInput.id = 'selectedCardId';
        hiddenInput.name = 'selectedCardId';
        form.appendChild(hiddenInput);
    }
    hiddenInput.value = savedCardId;

    // Diğer form alanlarını doldur
    document.getElementById('cardNumber').value = cardNumber;
    document.getElementById('cardHolderName').value = cardHolderName;
    document.getElementById('expiryDate').value = expiryDate;

    // Form alanlarını devre dışı bırak
    document.getElementById('cardNumber').disabled = true;
    document.getElementById('cardHolderName').disabled = true;
    document.getElementById('expiryDate').disabled = true;

    // Kartı kaydet checkbox'ını gizle
    const saveCardDiv = document.querySelector('.form-check');
    if (saveCardDiv) {
        saveCardDiv.style.display = 'none';
    }

    // Modal'ı kapat
    const savedCardModal = bootstrap.Modal.getInstance(document.getElementById('savedCardModal'));
    if (savedCardModal) {
        savedCardModal.hide();
    }
}

// Function to update the card preview based on form inputs
function updateCardPreview() {
    const cardNumber = document.getElementById('cardNumber').value.replace(/\D/g, '');
    const cardHolderName = document.getElementById('cardHolderName').value;
    const expiryDate = document.getElementById('expiryDate').value;

    // Update preview elements
    document.getElementById('cardNumberPreview').innerText = cardNumber.replace(/\d(?=\d{4})/g, "*");
    document.getElementById('cardHolderNamePreview').innerText = cardHolderName;
    document.getElementById('expiryDatePreview').innerText = expiryDate;
}

function formatCardNumber(event) {
    const input = event.target;
    let formattedValue = input.value.replace(/\D/g, '').replace(/(.{4})(?=.)/g, '$1 ').trim();
    input.value = formattedValue;
}
function renderSavedCards(cards) {
    const savedCardsContainer = document.getElementById("savedCardsContainer");
    savedCardsContainer.innerHTML = ""; // Listeyi temizle

    cards.forEach(card => {
        const formattedExpiryDate = formatExpiryDate(card.expiryDate); // Tarihi formatla
        const cardElement = `
    <div class="saved-card">
        <p>Kart Numarası: ${card.cardNumber.replace(/\d(?=\d{4})/g, "*")}</p>
        <p>Kart Sahibi: ${card.cardHolderName}</p>
        <p>Son Kullanma Tarihi: ${formattedExpiryDate}</p>
    </div>
    `;
        savedCardsContainer.innerHTML += cardElement;
    });
}

function formatExpiryDate(event) {
    const input = event.target;
    let formattedValue = input.value.replace(/\D/g, '').slice(0, 4);
    if (formattedValue.length > 2) {
        formattedValue = formattedValue.slice(0, 2) + '/' + formattedValue.slice(2);
    }
    input.value = formattedValue;
}


// Return to form with reset modal content
function goBackToForm() {
    const modalBody = document.querySelector('#paymentModal .modal-body');
    modalBody.innerHTML = `
    <div id="paymentCardPreview">
        <div class="card-number" id="cardNumberPreview">**** **** **** ****</div>
        <div class="card-holder" id="cardHolderNamePreview">Kart Sahibinin Adı</div>
        <div class="expires">Expires: <span id="expiryDatePreview">MM/YY</span></div>
        <img src="https://upload.wikimedia.org/wikipedia/commons/0/04/Mastercard-logo.png" class="card-logo" alt="Card Logo">
    </div>
    <form method="post" action="@Url.Action(" SaveCard", "Payment")">
    <button type="submit" class="btn btn-success w-100 mt-3">Ödemeyi Tamamla</button>
</form>
`;
}
function showSuccessToast() {
    const toast = document.createElement('div');
    toast.className = 'position-fixed top-0 start-50 translate-middle-x p-3';
    toast.style.zIndex = '9999';
    toast.innerHTML = `
        <div class="toast align-items-center text-white bg-success border-0" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="d-flex">
                <div class="toast-body">
                    <i class="fas fa-check-circle me-2"></i>
                    Bilet başarıyla satın alındı!
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>
    `;
    document.body.appendChild(toast);

    const toastElement = new bootstrap.Toast(toast.querySelector('.toast'), {
        delay: 3000
    });
    toastElement.show();

    // 3 saniye sonra Biletlerim sayfasına yönlendir
    setTimeout(() => {
        window.location.href = '/Member/Ticket/Index';
    }, 3000);
}

// Mevcut form submit işleyicisini güncelle
document.querySelector('form').addEventListener('submit', async (e) => {
    e.preventDefault();

    try {
        const formData = new FormData(e.target);
        const response = await fetch(e.target.action, {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            showSuccessToast();
        } else {
            // Hata durumunda işlem
            console.error('Ödeme işlemi başarısız');
        }
    } catch (error) {
        console.error('Bir hata oluştu:', error);
    }
});