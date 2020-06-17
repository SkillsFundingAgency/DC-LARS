import { ResultsHelper } from '../helpers/resultsHelper';

describe('Results helper', () => {

    document.body.innerHTML = '<div id="count"></div><div id="results">Existing Results</div><div id="validationErrors"></div><div id="loadingImage">style="display:none"</div>';

    const sut = new ResultsHelper(document.getElementById("results") as HTMLElement,
                                    document.getElementById("count") as HTMLElement,
                                    document.getElementById("validationErrors") as HTMLElement,
                                    document.getElementById("loadingImage") as HTMLElement
    );

    test("SetCount should update count text with count", () => {
        sut.setCount(5);

        expect(document.getElementById("count")?.innerHTML).toBe('5 results');
    });

    test("SetIsLoading should clear existing search results results", () => {
        sut.setIsLoading();

        expect(document.getElementById("results")?.innerHTML).toBe('');
    });

    test("SetIsLoading should show loading element", () => {
        sut.setIsLoading();

        expect(document.getElementById("loadingImage")?.getAttribute("style")).toBe("display: block;");
    });

});


