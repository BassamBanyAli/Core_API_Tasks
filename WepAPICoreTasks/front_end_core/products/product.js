async function fetchAndDisplayProducts() {
    try {

        let id = Number(localStorage.getItem('id'));
        const url = `https://localhost:7167/api/Products/GetProductByCategoryID?id=${id}`;
        
      var token=localStorage.getItem("jwtToken");
      if(token==null)
        window.location.href="../Cart/Authentication/login.html";
        const response = await fetch(url,{
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        // Parse JSON data
        const data = await response.json();
        

        // Clear existing content in the container (if needed)
        const container = document.getElementById('card-container');
        container.innerHTML = '';

        // Iterate over the data and create card elements
        data.forEach(card => {
            const cardDiv = document.createElement('div');
            cardDiv.className = 'card';
            cardDiv.style.width = '18rem';
            cardDiv.style.marginBottom = '1rem';

            // Create and set the image element
            const img = document.createElement('img');
            img.className = 'card-img-top';
            img.src = 'https://cdn.corporatefinanceinstitute.com/assets/products-and-services-1024x1024.jpeg'; // Replace with actual image URL if available
            img.alt = 'Card image cap';
            cardDiv.appendChild(img);

            // Create the card body
            const cardBody = document.createElement('div');
            cardBody.className = 'card-body';

            // Create and set the card title
            const h5 = document.createElement('h5');
            h5.className = 'card-title';
            h5.textContent = card.productName || 'Default Title'; // Handle missing title
            cardBody.appendChild(h5);

            // Create and set the card text
            const p = document.createElement('p');
            p.className = 'card-text';
            p.textContent = card.description|| 'Default Description'; // Handle missing categoryName
            cardBody.appendChild(p);

            // Create and set the card link
            const a = document.createElement('a');
            a.href = "../product_details/details.html"; // Handle missing link
            a.className = 'btn btn-primary';
            a.textContent = 'Go to details';

            a.onclick = function() {
                localStorage.setItem('ProductID',card.productId);
                alert('Data stored in local storage');
            };
            cardBody.appendChild(a);

            // Append the card body to the card
            cardDiv.appendChild(cardBody);

            // Append the card to the container
            container.appendChild(cardDiv);
        });
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

// Call the function to fetch data and create cards
fetchAndDisplayProducts();
