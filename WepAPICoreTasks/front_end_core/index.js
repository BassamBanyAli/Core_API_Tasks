async function fetchAndDisplayCards() {
    try {
        const url = 'https://localhost:44323/api/Categories';
        const response = await fetch(url);
        const data = await response.json();

        data.forEach(card => {
            const cardDiv = document.createElement('div');
            cardDiv.className = 'card';
            cardDiv.style.width = '18rem';
            cardDiv.style.marginBottom = '1rem';

            const img = document.createElement('img');
            img.className = 'card-img-top';
            img.src = `../Task1_Core/uploads/${card.categoryImage}`;
            img.alt = 'Card image cap';
            cardDiv.appendChild(img);

            const cardBody = document.createElement('div');
            cardBody.className = 'card-body';

            const h5 = document.createElement('h5');
            h5.className = 'card-title';
            h5.textContent = card.title;
            cardBody.appendChild(h5);

            const p = document.createElement('p');
            p.className = 'card-text';
            p.textContent = card.categoryName;
            cardBody.appendChild(p);

            const storeButton = document.createElement('a');
            storeButton.href = './products/product.html';
            storeButton.className = 'btn btn-primary';
            storeButton.textContent = 'Store Data';
            storeButton.onclick = function() {
                localStorage.setItem('id', card.categoryId);
                alert('Data stored in local storage');
            };
            cardBody.appendChild(storeButton);

            // Add Edit button
            const editButton = document.createElement('a');
            editButton.href = `../EditCategory/Edit.html`;
            editButton.className = 'btn btn-secondary ml-2';
            editButton.textContent = 'Edit Category';
            editButton.onclick = function() {
                localStorage.setItem('EditID', card.categoryId);
                alert('Data stored in local storage')};
            cardBody.appendChild(editButton);

            cardDiv.appendChild(cardBody);
            document.getElementById('card-container').appendChild(cardDiv);
        });
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}


fetchAndDisplayCards();