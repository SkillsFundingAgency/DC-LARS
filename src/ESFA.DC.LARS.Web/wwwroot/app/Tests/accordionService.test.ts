import { accordionService } from '../Services/accordionService';

describe('toggleSection', () => {

	test('toggle when closed sets correct state', () => {

		// Arrange
		document.body.innerHTML = `<section class='filter-section'>
								<h2>
									<a id='sut-button' class='filter-box-button' aria-expanded='false'>
									</a>
								</h2> 
								<div id='panel' class='filter-box'>
									One
								</div>
							</section>`;

		// Act
		accordionService.toggleSection('sut-button', false);
		
		// Assert
		const button = document.getElementById('sut-button');
		expect(button?.getAttribute('aria-expanded')).toBe('true');
		expect(button?.classList.contains('rotate')).toBe(true);
		expect(document.getElementById('panel')?.classList.contains('expanded')).toBe(true);
	});

	test('toggle when open sets correct state', () => {

		// Arrange
		document.body.innerHTML = `<section class='filter-section'>
								<h2>
									<a id='sut-button' class='filter-box-button rotate' aria-expanded='true'>
									</a>
								</h2> 
								<div id='panel' class='filter-box expanded'>
									One
								</div>
							</section>`;

		// Act
		accordionService.toggleSection('sut-button', false);

		// Assert
		const button = document.getElementById('sut-button');
		expect(button?.getAttribute('aria-expanded')).toBe('false');
		expect(button?.classList.contains('rotate')).toBe(false);
		expect(document.getElementById('panel')?.classList.contains('expanded')).toBe(false);
	});
})

describe('updateAccordionAll', () => {

	const filterHTML = `<div><button id='accordionButton' {{state}}></button>
								<section class='filter-section'>
									<h2>
										<a id='sut-button-1' class="filter-box-button" aria-expanded='false'>
										</a>
									</h2> 
									<div id='panel-1' class="filter-box">
										One
									</div>
								</section>
								<section class='filter-section'>
									<h2>
										<a id='sut-button-2' class="filter-box-button rotate" aria-expanded='true'>
										</a>
									</h2> 
									<div id='panel-2' class="filter-box expanded">
										Two
									</div>
								</section></div>`;

	test('sets all to closed when closing', () => {
		// Arrange
		document.body.innerHTML = filterHTML.replace('{{state}}', `class='close-all'`);

		// Act
		accordionService.updateAccordionAll();

		// Assert
		expect(document.querySelectorAll('[aria-expanded="false"]')?.length).toBe(document.querySelectorAll('.filter-box-button')?.length);
		expect(document.querySelectorAll('.expanded')?.length).toBe(0);
		expect(document.querySelectorAll('.rotate')?.length).toBe(0);
	});


	test('sets all to open when closing', () => {
		// Arrange
		document.body.innerHTML = filterHTML.replace('{{state}}', `class='open-all'`);

		// Act
		accordionService.updateAccordionAll();

		// Assert
		const accordionCount = document.querySelectorAll('section')?.length;
		expect(document.querySelectorAll('[aria-expanded="true"]')?.length).toBe(accordionCount);
		expect(document.querySelectorAll('.expanded')?.length).toBe(accordionCount);
		expect(document.querySelectorAll('.rotate')?.length).toBe(accordionCount);
	});
});