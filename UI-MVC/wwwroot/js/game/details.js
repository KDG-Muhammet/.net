document.addEventListener("DOMContentLoaded", function() {
    const updateButton = document.getElementById("updateButton");
    const updateRating = document.getElementById("updateRating");
    const gameId = document.getElementById("Id");
    
    console.log("Game ID:", gameId.innerText);

    updateButton.addEventListener("click", function () {
        const newRating = prompt("Enter the new rating:");
        updateGameRating(gameId,newRating)
    });
    function updateGameRating(gameId, newRating) {
        console.log("Game ID:", gameId.innerText);
        let rating = parseInt(newRating);
        console.log(rating);
        fetch(`/api/games/` + gameId.innerText, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({Id: gameId, Rating: rating })
        })
            .then(response =>  response.json())
            .then(data => {
                updateRating.textContent = newRating;
            })
            .catch(error => {
                console.error("Error:", error);
                alert("Failed to update rating");
            });
    }
});