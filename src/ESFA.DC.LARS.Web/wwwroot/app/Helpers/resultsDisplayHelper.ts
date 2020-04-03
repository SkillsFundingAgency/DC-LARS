import { ISearchResults } from '../Interfaces/ISearchResults';

export class ResultsDisplayHelper {

	constructor(private resultsContainer: HTMLElement, private resultsCountContainer: HTMLElement, private validationErrorContainer:HTMLElement) {
	}

	public SetIsLoading(): void {
		this.SetInnerHtml(this.resultsContainer, "Loading");
	}

	public UpdateForResponse(response: ISearchResults): void {
		this.SetResults(response.data);
		this.SetCount(response.count);
		this.SetValidationErrors(response.validationErrors);
	}

	public SetResults(html: string): void {
		this.SetInnerHtml(this.resultsContainer, html);
	}

	public SetCount(count: number): void {
		this.SetInnerHtml(this.resultsCountContainer, `${count} results`);
	}

	public SetValidationErrors(errors: Array<string>): void {
		var html = errors.map(v => `<li class="govuk-error-message">${v}</li>`).join();
		this.SetInnerHtml(this.validationErrorContainer, html);
	}

	private SetInnerHtml(element: HTMLElement, html: string): void {
		if (element) {
			element.innerHTML = html;
		}
	}
}