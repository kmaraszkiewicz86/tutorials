import { NO_ERRORS_SCHEMA } from "@angular/core";
import { ComponentFixture, TestBed } from "@angular/core/testing"
import { By } from "@angular/platform-browser";
import { HeroComponent } from "./hero.component"

describe('HeroComponent (shallow test)', () => {

    let fixture: ComponentFixture<HeroComponent>;

    beforeEach(() => {
        TestBed.configureTestingModule({
            declarations: [HeroComponent],
            schemas: [NO_ERRORS_SCHEMA]
        });

        fixture = TestBed.createComponent(HeroComponent);
    })

    it('shoul have the correct hero', () => {
        fixture.componentInstance.hero = { id: 1, name: 'hero1', strength: 3 };

        expect(fixture.componentInstance.hero.name).toEqual('hero1');
    })

    it('should render hero name in the anchor tag', () => {
        fixture.componentInstance.hero = { id: 1, name: 'hero1', strength: 3 };
        fixture.detectChanges();

        let debugElementOfAnchor = fixture.debugElement.query(By.css('a'))

        expect(debugElementOfAnchor.nativeElement.textContent).toContain('hero1')

        expect(fixture.nativeElement.querySelector('a').textContent).toContain('hero1')
    });

})