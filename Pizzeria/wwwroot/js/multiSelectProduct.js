$(document).ready(function () {

    let selectedObject = [];
    let selectedIds = [];

    $("#IngredientiAggiunti").change(function () {
        var selected = $(this).find('option:selected');
        selected.each(function () {
            if (selectedIds.includes($(this).val())) {
                return;
            }
            else {
                let obj = {
                    id: $(this).val(),
                    nome: $(this).text()
                }
                selectedObject.push(obj);
                selectedIds.push($(this).val());
                console.log(selectedObject);
            }
        });
        $("#listaIngredientiScelti").empty();
        $.each(selectedObject, function (index, obj) {
            let divElement = createSelected(obj);

            $("#listaIngredientiScelti").append(divElement);
        });
        $("#IngredientiAggiuntiHidden").val(selectedIds);

    });

    function removeFromSelected(id) {
        selectedObject = selectedObject.filter(function (obj) {
            return obj.id !== id;
        });
        selectedIds = filterValues(selectedIds, id);

        $("#listaIngredientiScelti").empty();
        $.each(selectedObject, function (index, obj) {
            let divElement = createSelected(obj);
            $("#listaIngredientiScelti").append(divElement);
        });

        $("#IngredientiAggiuntiHidden").val(selectedIds);
    }

    function createSelected(obj) {
        let divElement = document.createElement("div");
        divElement.classList.add("d-flex");

        let paragraphElement = document.createElement("p");
        paragraphElement.classList.add("me-3");
        paragraphElement.classList.add("mb-0");
        paragraphElement.textContent = obj.nome;

        let spanElement = document.createElement("span");
        spanElement.style.color = "red";
        spanElement.textContent = "X";
        spanElement.style.cursor = "pointer";
        spanElement.onclick = function () {
            removeFromSelected(obj.id);
        }

        divElement.appendChild(paragraphElement);
        divElement.appendChild(spanElement);

        return divElement;
    }

    function filterValues(values, id) {
        let filteredValues = [];
        for (let i = 0; i < values.length; i++) {
            if (values[i] !== id) {
                filteredValues.push(values[i]);
            }
        }
        return filteredValues;
    }
});