import { ISearchResults } from '../Interfaces/ISearchResults';

export class ResultsHelper {

	constructor(private resultsContainer: HTMLElement, private resultsCountContainer: HTMLElement, private validationErrorContainer: HTMLElement, private loadingContainer: HTMLElement) {
	}

	public setIsLoading(): void {
		this.setInnerHtml(this.resultsContainer, "");
		this.setLoadingDisplay("block");
	}

	public updateForResponse(response: ISearchResults): void {
		this.setResults(response.data);
		this.setCount(response.count);
		this.setValidationErrors(response.validationErrors);
	}

	public setResults(html: string): void {
		this.setLoadingDisplay("none");
		this.setInnerHtml(this.resultsContainer, html);
	}

	public setCount(count: number): void {
		this.setInnerHtml(this.resultsCountContainer, `${count} results`);
		if (this.resultsCountContainer?.style.visibility === "hidden") {
			this.resultsCountContainer.style.visibility = "visible";
		}
	}

	public setValidationErrors(errors: Array<string>): void {
		const html = errors.map(v => `<li class="govuk-error-message">${v}</li>`).join();
		this.setInnerHtml(this.validationErrorContainer, html);
	}

	private setLoadingDisplay(display: string) : void {
		if (this.loadingContainer) {
			this.loadingContainer.style.display = display;
		}
	}

	private setInnerHtml(element: HTMLElement, html: string): void {
		if (element) {
			element.innerHTML = html;
		}
	}
}