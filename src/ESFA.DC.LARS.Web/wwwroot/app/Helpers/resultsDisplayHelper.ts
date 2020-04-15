import { ISearchResults } from '../Interfaces/ISearchResults';

export class ResultsDisplayHelper {

	constructor(private resultsContainer: HTMLElement, private resultsCountContainer: HTMLElement, private validationErrorContainer:HTMLElement) {
	}

	public setIsLoading(): void {
		this.setInnerHtml(this.resultsContainer, "Loading");
	}

	public updateForResponse(response: ISearchResults): void {
		this.setResults(response.data);
		this.setCount(response.count);
		this.setValidationErrors(response.validationErrors);
	}

	public setResults(html: string): void {
		this.setInnerHtml(this.resultsContainer, html);
	}

	public setCount(count: number): void {
		this.setInnerHtml(this.resultsCountContainer, `${count} results`);
	}

	public setValidationErrors(errors: Array<string>): void {
		var html = errors.map(v => `<li class="govuk-error-message">${v}</li>`).join();
		this.setInnerHtml(this.validationErrorContainer, html);
	}

	private setInnerHtml(element: HTMLElement, html: string): void {
		if (element) {
			element.innerHTML = html;
		}
	}
}