document.addEventListener('DOMContentLoaded', function () {
    // Fetch all records when the page loads
    fetchCompanies();

    // Create a button element
    const reloadButton = document.createElement('button');
    reloadButton.textContent = 'Reload Data';

    // Append the button to the container div
    const buttonContainer = document.getElementById('buttonContainer');
    buttonContainer.appendChild(reloadButton);

    // Add an event listener to the button for reloading data
    reloadButton.addEventListener('click', function () {
        fetchCompanies();
    });

    function fetchCompanies() {
        fetch('/api/Companies', {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        })
            .then(function (response) {
                if (response.ok)
                    return response.json(); // deserialize data
            })
            .then(function (data) {
                displayData(data)
            })
    }
    function displayData(companies) {
        // Create a table element
        const table = document.createElement('table');
        table.classList.add('table');
        
        // Create a table header
        const headerRow = document.createElement('tr');
        const nameHeader = document.createElement('th');
        nameHeader.textContent = 'Name';
        headerRow.appendChild(nameHeader);

        const addressHeader = document.createElement('th');
        addressHeader.textContent = 'Address';
        headerRow.appendChild(addressHeader);

        const yearHeader = document.createElement('th');
        yearHeader.textContent = 'Year Founded';
        headerRow.appendChild(yearHeader);

        table.appendChild(headerRow);

        // Create table rows and cells for each company
        for (let company of companies) {
            const row = document.createElement('tr');

            const nameCell = document.createElement('td');
            nameCell.textContent = company.name;
            row.appendChild(nameCell);

            const addressCell = document.createElement('td');
            addressCell.textContent = company.address;
            row.appendChild(addressCell);

            const yearCell = document.createElement('td');
            yearCell.textContent = company.yearFounded.toString(); 
            row.appendChild(yearCell);

            table.appendChild(row);
        }

        // Append the table to the body or any other desired container
        const companiesContainer = document.getElementById('companies');
        companiesContainer.innerHTML = ''; // Clear existing content
        companiesContainer.appendChild(table);
    }
});