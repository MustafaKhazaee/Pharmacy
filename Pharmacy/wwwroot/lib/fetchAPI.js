class FetchAPI {

    async getUpdateModal(url, id, callBack) {
        url = url + `?id=${id}`;
        return await fetch(url, {
            method: "get",
        }).then(response => {
            if (response.status == 200) {
                callBack.call();
            }
            return response.text();
        }).then(data => data).catch(error => console.error(error));
    }

    async put(url, form, callBack) {
        const data = await this.getFormDataFromForm(form);
        return await fetch(url, {
            method: "put",
            body: data,
        }).then(response => {
            if (response.status == 200) {
                callBack.call();
            }
            return response.json();
        }).then(data => data).catch(error => console.error(error));
    }

    async post(url, form, callBack) {
        const data = await this.getFormDataFromForm(form);
        return await fetch(url, {
            method: "post",
            body: data,
        }).then(response => {
            if (response.status == 200) {
                callBack.call();
            }
            return response.json();
        }).then(data => data).catch(error => console.error(error));
    }

    async getFormDataFromForm(form) {
        const a = new FormData();
        for (let i = 0; i < form.length; i++) {
            const input = form[i];
            if (input.type.toString() === 'text' || input.type.toString() === 'password') {
                a.append(input.name, input.value);
            } else if (input.type.toString() === 'file') {
                a.append(input.name, input.files[0]);
            } else if (input.type.toString() === 'checkbox') {
                a.append(input.name, input.checked);
            }
        }
        return a;
    }

    async delete(url, id, callBack) {
        url = url + `?id=${id}`;
        await fetch(url, {
            method: "DELETE",
        }).then(response => {
            if (response.status == 200) {
                callBack.call();
            }
            return response.json();
        }).then(data => data).catch(error => console.error(error));
    }
}


 //async getJSONFromForm(form) {
 //    const a = {};
 //    for (let i = 0; i < form[0].length; i++) {
 //        const input = form[0][i];
 //        a[input.name] = input.value;
 //    }
 //    return a;
 //}