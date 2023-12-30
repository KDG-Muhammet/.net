window.addEventListener("load", () => init())
let storeId; 
let gameId;
function init(){
    storeId = document.getElementById("Id")
    console.log("Store ID:", storeId.innerText);

    console.log(storeId)
    fetchRelatedData();

    // Fetch and populate select-box
    fetchGames();

    // Add event listener for the "Add" button
    const addButton = document.getElementById('addButton');
    addButton.addEventListener('click', function () {
        // Handle "Add" button click
        addRecord();
    });
}

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
       }).catch(error => alert('Oeps, something went wrong!'));
       
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
        priceCell.textContent = game.price !== null ? "$" + game.price : "unknown";
        row.appendChild(priceCell);

        const genreCell = document.createElement('td');
        genreCell.appendChild(document.createTextNode(enumToPosition(game.genre)))
        row.appendChild(genreCell);

        const yearCell = document.createElement('td');
        yearCell.textContent = game.yearReleased; // Assuming yearFounded is a DateOnly instance
        row.appendChild(yearCell);
        

        const ratingCell = document.createElement('td');
        ratingCell.textContent = game.rating.toString(); // Assuming yearFounded is a DateOnly instance
        row.appendChild(ratingCell);

        table.appendChild(row);
    }
    relatedDataContainer.innerHTML = ''; // Clear existing content
    relatedDataContainer.appendChild(table);
}

function addRecord() {
    const gameSelect = document.getElementById('gameSelect');
    storeId = document.getElementById("Id")
    console.log("Store ID:", storeId.innerText);
    // Get selected game ID and additional information
    gameId = gameSelect.value;
    console.log("Adding record for Store ID:", storeId.innerText, "and Game ID:", gameId);
    
    // Make a POST request to add a new record to the many-to-many relationship
    fetch('/api/Stores' , {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'

        },
        body: JSON.stringify({gameDto: gameId, storeDto: storeId.innerText })
    })
        .then(response => response.json())
        .then(data => {
            // Refresh the related data and select box
            fetchRelatedData(data);
        }).catch(error => alert('The selected game is already in the store.'));

}

function fetchGames() {
    fetch('/api/Games', {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => populateSelectBox(data))
}

function populateSelectBox(games) {
    const gameSelect = document.getElementById('gameSelect');

    // Clear existing options
    gameSelect.innerHTML = '';

    // Populate select box with game options
    games.forEach(game => {
        const option = document.createElement('option');
        option.value = game.id;
        option.textContent = game.name;
        gameSelect.appendChild(option);
    });
    
}