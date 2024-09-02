async function details(event) {

    var input=document.getElementById("input").value;
    var url=`https://localhost:7167/getname${input}`;
    var response= await fetch(url);
    var data= await response.json();


    var container=document.getElementById("container");
    container.innerHTML=`<div class="card" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">${data.userId}</h5>
    <h6 class="card-subtitle mb-2 text-muted">${data.username}</h6>
    <p class="card-text">${data.email}</p>
    <a href="#" class="card-link">Card link</a>
    <a href="#" class="card-link">Another link</a>
  </div>
</div>`



    
    
}