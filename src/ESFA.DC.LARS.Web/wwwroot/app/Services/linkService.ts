export default class LinkService {

    setSearchResultsLink(id: string, searchTerm: string, teachingYear: string) {
        const anchor = document.getElementById(id) as HTMLAnchorElement;
        anchor.href = `/SearchResult?SearchTerm=${searchTerm}&TeachingYear=${teachingYear}`;
    }

    setLearningAimDetailLink(id: string, learnAimRef: string, academicYear: string) {
        const anchor = document.getElementById(id) as HTMLAnchorElement;
        anchor.href = `/LearningAimDetails/${learnAimRef}?academicYear=${academicYear}`;
    }

    setFrameworksLink(id: string, learnAimRef: string) {
        const anchor = document.getElementById(id) as HTMLAnchorElement;
        anchor.href = `/Frameworks/${learnAimRef}`;
    }

}