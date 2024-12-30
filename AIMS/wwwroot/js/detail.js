const mainImage = document.querySelector('.main-image img');
const thumbnails = document.querySelectorAll('.thumbnail-images img');

thumbnails.forEach(thumbnail => {
    thumbnail.addEventListener('click', () => {
        mainImage.src = thumbnail.src;
        thumbnails.forEach(t => t.classList.remove('active'));
        thumbnail.classList.add('active');
    });
});
const addToCartForm = document.querySelector('.add-to-cart').closest('form');

// Bắt sự kiện submit form
addToCartForm.addEventListener('submit', (event) => {
    // Lấy input số lượng
    const quantityInput = addToCartForm.querySelector('input[name="quantity"]');

    // Lấy input mediaQuantity
    const mediaQuantityInput = addToCartForm.querySelector('input[name="mediaQuantity"]');

    // Gán giá trị của quantity vào mediaQuantity
    mediaQuantityInput.value = quantityInput.value;
});