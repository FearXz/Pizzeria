$(document).ready(function () {
    async function FetchListaProdotti() {

        const response = await fetch('UserOrder/FetchListaProdotti');
        const data = await response.json();
        console.log(data);

        $('#listaProdotti').empty(); // Clear the existing content

        data.forEach(product => {
            const col = $('<div>').addClass('col-6 mb-3');
            // Creazione del div principale con classe "card" e stile "max-width: 450px;"
            const card = $('<div>').addClass('row bg-dark border border-1 rounded-2').css('max-width', '450px');
            col.append(card);
            // Colonna per l'immagine
            const imgCol = $('<div>').addClass('col-auto');
            card.append(imgCol);

            // Immagine
            const img = $('<img>').addClass('fix-w-200 fix-h-120 cover my-3').attr('src', product.imgProdotto);
            imgCol.append(img);

            // Colonna per il testo e i bottoni
            const textAndButtonsCol = $('<div>').addClass('col-auto');
            card.append(textAndButtonsCol);

            // Titolo della card
            const title = $('<h5>').addClass('mb-5 mt-3').text(product.nomeProdotto);
            textAndButtonsCol.append(title);

            // Div per i bottoni
            const buttonDiv = $('<div>').addClass('');
            textAndButtonsCol.append(buttonDiv);

            // Bottone "Aggiungi"
            const addButton = $('<button>').addClass('btn btn-brown-200 me-3').text('Aggiungi');
            addButton.on('click', async function () {
                FetchAddToCartSession(product);
            });
            buttonDiv.append(addButton);

            // Bottone "Rimuovi"
            const removeButton = $('<button>').addClass('btn btn-brown-200').text('Rimuovi');
            removeButton.on('click', async function () {
                FetchRemoveFromCartSession(product.idProdotto);
            });
            buttonDiv.append(removeButton);

            // Aggiunta della card al contenitore desiderato
            $('#listaProdotti').append(col);



        });
    }
    FetchListaProdotti();

    async function FetchAddToCartSession(product) {
        try {
            const response = await fetch('UserOrder/FetchAddToCartSession/' + product.idProdotto);
            if (response.ok) {
                location.reload();
            }


        }
        catch (error) {
            console.log(error);
        }

    }


    async function FetchRemoveFromCartSession(idProdotto) {
        try {
            const response = await fetch('UserOrder/FetchRemoveFromCartSession/' + idProdotto);

            if (response.ok) {
                location.reload();
            }
        }
        catch (error) {
            console.log(error);
        }
    }

});