class AccordionService {

    public initialiseAccordion() : void {
        const classScope = this;
        const button = document.getElementById('accordionButtonContainer');
        if (button) {
            button.removeAttribute("style");
        }

        document.querySelectorAll('.filter-box-button').forEach(button => {
            classScope.toggleSection(button.id, true);
        });
    }

    public updateAccordionAll() : void {
        const button = document.getElementById('accordionButton');
        const panels = document.querySelectorAll('.filter-box');
        const panelButtons = document.querySelectorAll('.filter-box-button');

        if (button) {
            if (button.classList.contains('close-all')) {
                panels.forEach(panel => {
                    panel.classList.remove('expanded');
                });

                panelButtons.forEach(panelButton => {
                    panelButton.classList.remove('rotate');
                });

                button.innerHTML = 'Open all';
            } else {
                panels.forEach(panel => {
                    panel.classList.add('expanded');
                });

                panelButtons.forEach(panelButton => {
                    panelButton.classList.add('rotate');
                });

                button.innerHTML = 'Close all';
            }

            button.classList.toggle('close-all');
        }
    }

    public toggleSection(id: string, init: boolean) : void {
        const button = document.getElementById(id);
        const accordionButton = document.getElementById('accordionButton');
        
        if (button) {
            const panel = <HTMLElement>button.nextElementSibling;

            if (panel) {
                if (!init) {
                    button.classList.toggle('rotate');
                }

                panel.classList.toggle('expanded');
            }
        }

        if (accordionButton) {
            if (this.allPanelsOpen()) {
                if (!accordionButton.classList.contains('closed')) {
                    accordionButton.classList.add('closed');
                }
                accordionButton.innerHTML = 'Close all';
            }

            if (this.allPanelsClosed()) {
                if (accordionButton.classList.contains('closed')) {
                    accordionButton.classList.remove('closed');
                }
                accordionButton.innerHTML = 'Open all';
            }
        }
    }

    private allPanelsOpen(): boolean {
        const panels = document.querySelectorAll('.filter-box');

        let allOpen: boolean = true;
        panels.forEach(panel => {
            if (!panel.classList.contains('expanded')) {
                allOpen = false;
            }
        });

        return allOpen;
    }

    private allPanelsClosed(): boolean {
        const panels = document.querySelectorAll('.filter-box');

        let allClosed: boolean = true;
        panels.forEach(panel => {
            if (panel.classList.contains('expanded')) {
                allClosed = false;
            }
        });

        return allClosed;
    }
}

export const accordionService = new AccordionService();