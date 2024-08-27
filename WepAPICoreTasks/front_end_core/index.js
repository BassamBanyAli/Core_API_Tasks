async function fetchAndDisplayCards() {
    try {
        const url = 'https://localhost:44323/api/Categories';

       // const queryString = window.location.search; // Get the query string part of the URL
        //const urlParams = new URLSearchParams(queryString);

        // Get the value of a specific query parameter
       // const id = urlParams.get('id'); // 'John'
        //console.log(id);

        // const xresponse = await fetch(url+`/${id}`);
        // const xdata = await xresponse.json();
        // console.log(xdata);
        


        
        const response = await fetch(url);
        const data = await response.json();


        data.forEach(card => {

            const cardDiv = document.createElement('div');
            cardDiv.className = 'card';
            cardDiv.style.width = '18rem';
            cardDiv.style.marginBottom = '1rem';


            const img = document.createElement('img');
            img.className = 'card-img-top';
            img.src ='https://letsenhance.io/static/73136da51c245e80edc6ccfe44888a99/1015f/MainBefore.jpg';
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
            const a = document.createElement('a');
            a.href = './products/product.html'; 
            a.className = 'btn btn-primary';
            a.textContent = 'Store Data';


            a.onclick = function() {
                localStorage.setItem('id',card.categoryId);
                alert('Data stored in local storage');
            };
            cardBody.appendChild(a);

   
            cardDiv.appendChild(cardBody);


            document.getElementById('card-container').appendChild(cardDiv);
        });
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}


fetchAndDisplayCards();