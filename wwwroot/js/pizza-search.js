import fakeFetch from './fake-fetch.js';

const useRealAPI = true;

async function fetchJSON(url, options) {
    if (useRealAPI) {
            const response = await fetch(url, options);
            const json = await response.json();
            return json;
    }
    else {
        // Sleep for one second to simulate a network delay.
        await new Promise((r) => setTimeout(r, 1000));
        const json = fakeFetch(url, options);
        return json;
    }
}

//Event-listener DOM, to make sure page is loaded. Don't know if needed
document.addEventListener('DOMContentLoaded', () => {
    const goesWithFoodElement = document.querySelector('#goesWithFood');
    const message = document.querySelector('#message');
    const resultList = document.querySelector('#recommendation');

    function displayResults(result) {
        if (result.length === 0) {
            message.hidden = false;
            resultList.hidden = true;
        } else {
            message.hidden = true;
            resultList.hidden = false;

            resultList.replaceChildren();

          /*  for (const hit of hits) {*/
                const pizzaName = document.createElement('h4');
                pizzaName.setAttribute("class", "pizzaName");

                const img = document.createElement('img');

                const ingredientHeader = document.createElement('h4');
                ingredientHeader.textContent = 'Ingredients'

                const ingredientList = document.createElement('ul');
                ingredientList.setAttribute("class", "ingredientList");

                /*const ingredientString = hit.ingredients;*/

                //retrieve the string from the API response and split into an array
                var ingredientsArray = result.ingredients;

                // Iterate over the array of words
                for (var i = 0; i < ingredientsArray.length; i++) {
                    // Trim each word to remove any leading or trailing spaces
                    var ingredient = ingredientsArray[i].trim();

                    // Create an li element for each word
                    var ingredientLi = document.createElement('li');

                    // Set the text content of the li element to the ingredient
                    ingredientLi.textContent = ingredient;

                    // Append the li element to the ul element
                    ingredientList.appendChild(ingredientLi);
                }

                img.src = result.photo.toString();
                img.style.width = '250px';
             
                pizzaName.textContent = result.name;

                const li = document.createElement('li');
                li.append(pizzaName);
                li.append(img);
                li.append(ingredientHeader);
                li.append(ingredientList);

                resultList.append(li);
        }
    }

    async function searchPizzas(categoryname) {
        try { 
        const result = await fetchJSON(
            'https://pizzaproject3.azurewebsites.net/api?' + new URLSearchParams({ categoryname }).toString()
        );

        displayResults(result);
        }
          
        catch {
            const message = document.querySelector('#message');
            message.hidden = false;
        }

    }

    // When the page is loaded, fetch data from the API.
    const string = goesWithFoodElement.textContent.trim();
    const trimmedString = string.split(',')[0];
    searchPizzas(trimmedString);


    // When the form is submitted, fetch data from the API.

    /*const form = document.querySelector('#pizza-form');*/
    const form = document.querySelector('form');

    form.addEventListener('submit', (event) => {
        event.preventDefault();
        const formData = new FormData(event.target);
        const pizzacategory = formData.get('category');
        searchPizzas(pizzacategory);
    });
});


//import fakeFetch from './fake-fetch.js';


//const useRealAPI = false;

//async function fetchJSON(url, options) {
//    if (useRealAPI) {
//        const response = await fetch(url, options);
//        const json = await response.json();
//        return json;
//    }
//    else {
//        // Sleep for one second to simulate a network delay.
//        await new Promise(r => setTimeout(r, 1000));
//        const json = fakeFetch(url, options);
//        return json;
//    }
//}


//const goesWithFoodElement = document.querySelector('#goesWithFood');
//const pizzacategory = goesWithFoodElement.textContent.trim();
//const message = document.querySelector('#message');

//const pizzadiv = document.querySelector('#pizzaGridCell')
//var pizzaname = document.querySelector('#pizzaName')
//var ingredientlist = document.querySelector('#ingredientlist')
//var resultList = document.querySelector('#recommendation')


//    const result = await fetchJSON(
//        'https://pizzaexample.com/api/?' +
//        new URLSearchParams({
//            q: pizzacategory,
//        }).toString()
//    );

//    if (result.hits.length === 0) {
//        message.hidden = false;
//        resultList.hidden = true;
//    }
//    else {
//        message.hidden = true;
//        resultList.hidden = false;

//        resultList.replaceChildren();

//        for (const hit of result.hits) {
//            const img = document.createElement('img');
//            img.src = hit.webformatURL;
//            img.style.width = '250px';

//            const li = document.createElement('li');
//            li.append(img);

//            resultList.append(li);
//        }
//    }
