export default {
    baseUrl: "https://localhost:7213/api/todoitems",
    getToDoItems: function (callback) {
        let url = this.baseUrl;
        let fetchPromise = fetch(url); 
        let jsonPromise = fetchPromise.then(response => response.json()); 
        jsonPromise.then(json => { callback(json) }); 
    },

    addTodoItem: function (description, callback) {
        let url = this.baseUrl;

        let payload = {
            "task": description,
            "isComplete": false
        };

        let fetchPromise = fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json"
            },
            body: JSON.stringify(payload)
        });

        let jsonPromise = fetchPromise.then(response => response.json()); 
        jsonPromise.then(json => { callback(json) }); 
    },

    updateTodoItem: function (id, description, complete, callback) {
        let url = this.baseUrl + `/${id}`;

        let payload = {
            "task": description,
            "todoItemId": id,
            "isComplete": complete
        };

        console.log(payload);

        let fetchPromise = fetch(url, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json"
            },
            body: JSON.stringify(payload)
        });

        let jsonPromise = fetchPromise.then(response => response.json()); 
        jsonPromise.then(json => { callback(json) }); 
    },

    setTodoItemCompleteStatus: function (id, complete, callback) {
        let url = this.baseUrl + `/${id}/${complete}`;
        
        let payload = {
            "isComplete": complete,
            "id": id
        };

        let fetchPromise = fetch(url, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json"
            },
            body: JSON.stringify(payload)
        });

        let jsonPromise = fetchPromise.then(response => response.json()); 
        jsonPromise.then(json => { callback(json) }); 
    },

    deleteToDoItem: function (id, callback) {
        let url = this.baseUrl + `/${id}`;

        let fetchPromise = fetch(url, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json"
            }
        });

        let jsonPromise = fetchPromise.then(response => callback());


    }
};
