import { NO_ERRORS_SCHEMA } from "@angular/core";
import { ComponentFixture, TestBed } from "@angular/core/testing"
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

})