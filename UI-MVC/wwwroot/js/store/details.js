window.addEventListener("load", () => init())
let storeId; 
function init(){
    storeId = document.getElementById("Id")
    // Fetch and display related data
    console.log(storeId)
    fetchRelatedData();

    // Fetch and populate select-box

    // Add event listener for the "Add" button
    const addButton = document.getElementById('addButton');
    addButton.addEventListener('click', function () {
        // Handle "Add" button click
        addRecord();
    });
}
document.addEventListener('DOMContentLoaded', function () {

});

async function fetchRelatedData() {
    // Use fetch to get related data and display it in the #relatedData element
    // Example: fetch('/api/authors/1/books')
   await fetch('/api/Stores/' + storeId.innerText, {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    }) .then((response) => {
       if (response.ok)
           return response.json();
   })
       .then(async (data) => {
           await fetchAvailableRecords(data);
       })
       
}

async function fetchAvailableRecords(data) {
    // Use fetch to get available records and populate the select-box
    // Example: fetch('/api/books/available')
    // Create a table element
    const relatedDataContainer = document.getElementById('relatedData');
    console.log(data)
    const table = document.createElement('table');
    table.classList.add('table');

    // Create a table header
    const headerRow = document.createElement('tr');
    
    const nameHeader = document.createElement('th');
    nameHeader.textContent = 'Name';
    headerRow.appendChild(nameHeader);

    const priceHeader = document.createElement('th');
    priceHeader.textContent = 'Price';
    headerRow.appendChild(priceHeader);

    const genreHeader = document.createElement('th');
    genreHeader.textContent = 'Genre';
    headerRow.appendChild(genreHeader);

    const yearHeader = document.createElement('th');
    yearHeader.textContent = 'YearReleased';
    headerRow.appendChild(yearHeader);

    const ratingHeader = document.createElement('th');
    ratingHeader.textContent = 'Rating';
    headerRow.appendChild(ratingHeader);

    table.appendChild(headerRow);

    const genre = ["Action", "Adventure","Horror"]
    function enumToPosition(num) {
        return genre[num - 1]
    }
    
    // Create table rows and cells for each company
    for (let game of data) {
        const row = document.createElement('tr');

        const nameCell = document.createElement('td');
        nameCell.textContent = game.name;
        row.appendChild(nameCell);

        const priceCell = document.createElement('td');
        priceCell.textContent = game.Price;
        row.appendChild(priceCell);

        const yearCell = document.createElement('td');
        yearCell.textContent = game.YearReleased; // Assuming yearFounded is a DateOnly instance
        row.appendChild(yearCell);


        const genreCell = document.createElement('td');
        genreCell.textContent = game.genre.toString(); // Assuming yearFounded is a DateOnly instance
        genreCell.appendChild(document.createTextNode(enumToPosition(game['genre'])))
        row.appendChild(genreCell);


        const ratingCell = document.createElement('td');
        ratingCell.textContent = game.rating.toString(); // Assuming yearFounded is a DateOnly instance
        row.appendChild(ratingCell);

        table.appendChild(row);
    }
    relatedDataContainer.innerHTML = ''; // Clear existing content
    relatedDataContainer.appendChild(table);
}

function addRecord() {
    // Use fetch to add a new record to the junction entity
    // Example: fetch('/api/authors/1/books', { method: 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' } })

    // After adding, refresh the related data and available records
    
    
    fetchRelatedData();
    fetchAvailableRecords();
}