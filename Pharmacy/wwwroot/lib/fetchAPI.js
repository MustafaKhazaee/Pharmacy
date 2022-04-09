class FetchAPI {

   async post(url, form, callBack) {
        const data = await this.getFormDataFromForm(form);
        await fetch(url, {
            method: "POST",
            body: data,
        }).then(response => {
            if (response.status == 200) {
                callBack.call();
            }
            return response.json();
        }).then(data => data).catch(error => console.error(error));
    }

    async getJSONFromForm(form) {
        const a = {};
        for (let i = 0; i < form[0].length; i++) {
            const input = form[0][i];
            a[input.name] = input.value;
        }
        return a;
    }

    async getFormDataFromForm(form) {
        const a = new FormData();
        for (let i = 0; i < form[0].length; i++) {
            const input = form[0][i];
            if (input.type.toString() === 'text') {
                a.append(input.name, input.value);
            } else if (input.type.toString() === 'file') {
                a.append(input.name, input.files[0]);
            }
        }
        return a;
    }
}