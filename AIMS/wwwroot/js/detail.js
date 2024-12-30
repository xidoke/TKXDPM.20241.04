const mainImage = document.querySelector('.main-image img');
const thumbnails = document.querySelectorAll('.thumbnail-images img');

thumbnails.forEach(thumbnail => {
    thumbnail.addEventListener('click', () => {
        mainImage.src = thumbnail.src;
        thumbnails.forEach(t => t.classList.remove('active'));
        thumbnail.classList.add('active');
    });
});