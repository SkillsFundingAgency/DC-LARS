import { INameValue } from "../Interfaces/INameValue";

class FormHelper {
    public addInputElements(form: HTMLFormElement, formElements: Array<INameValue<string>>): void {
        formElements.forEach(e => this.addElement(e.name, e.value, 'input', form));
    }

    public addElement(name: string, value: string, type:string, form: HTMLFormElement): void {
        const element = <HTMLInputElement>(document.createElement(`${type}`));
        Object.assign(element, { name, value });
        form.appendChild(element);
    }
}

export const formHelper = new FormHelper();