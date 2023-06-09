export default function fakeFetch(urlString, options) {
    const url = new URL(urlString);


    if (url.hostname === 'pizzaproject3.azurewebsites.net' && url.pathname === '/api') {

        //Jag har bara behållt det här så länge. Den kommer aldrig att köras eftersom q aldrig= animal
        if (url.searchParams.get('q') === 'animal') {
            return {
                total: 4,
                totalHits: 4,
                hits: [
                    {
                        webformatURL: '/fake-images/redpanda.jpg'
                    },
                    {
                        webformatURL: '/fake-images/koala.jpg'
                    },
                    {
                        webformatURL: '/fake-images/panda.jpg'
                    },
                    {
                        webformatURL: '/fake-images/raccoon.jpg'
                    }
                ]
            }
        }

        // Har lagt lite förenklad hämtning här. Om Vegetarian finns i GoesWellWith så kommer bild på vegetarisk pizza alltid att visas 
        //(även om GoesWellWith även inkluderar Meat). Kanske inte optimalt men funkar. 
        //Skulle kunna vara bra att lägga in någon sorts randomizer, men gör inte det nu. 
        //Tänkte inte greja mer med detta nu förrän vi har ett riktigt API att fetcha

        else if (url.searchParams.get('q').includes('Vegetarian')) {
            return {
                total: 1,
                totalHits: 1,
                hits: [
                    {
                        webformatURL: '/fake-images/Vegetarian.jpg',
                        pizzaName: 'Veggie Pizza',
                        ingredients: 'tomato, cheese, olives'
                    }
                ]
            }
        }

        else if (url.searchParams.get('q').includes('Meat')) {
            return {
                total: 1,
                totalHits: 1,
                hits: [
                    {
                        webformatURL: '/fake-images/Meat.jpg',
                        pizzaName: 'Meat Pizza',
                        ingredients: 'tomato, cheese, ham'
                    }
                ]
            }
        }

        else if (url.searchParams.get('q').includes('Chicken')) {
            return {
                total: 1,
                totalHits: 1,
                hits: [
                    {
                        webformatURL: '/fake-images/Chicken.jpg',
                        pizzaName: 'Chicken Pizza',
                        ingredients: 'tomato, cheese, chicken'
                    }
                ]
            }
        }
        else if (url.searchParams.get('q').includes('Dessert')) {
            return {
                total: 1,
                totalHits: 1,
                hits: [
                    {
                        webformatURL: '/fake-images/Dessert.jpg',
                        pizzaName: 'Dessert Pizza',
                        ingredients: 'nutella, banana'
                    }
                ]
            }
        }
        else if (url.searchParams.get('q').includes('Fish')) {
            return {
                total: 1,
                totalHits: 1,
                hits: [
                    {
                        webformatURL: '/fake-images/Fish.jpg',
                        pizzaName: 'Fish Pizza',
                        ingredients: 'tomato, cheese, tuna'

                    }
                ]
            }
        }

        else {
            return {
                total: 0,
                totalHits: 0,
                hits: []
            }

        }
    }
    else {
        throw new Error('This URL has not been implemented in the fake API: ' + urlString);
    }
}
