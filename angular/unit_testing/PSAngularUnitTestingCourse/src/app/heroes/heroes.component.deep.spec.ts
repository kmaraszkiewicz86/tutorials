import { Component, Directive, Input, NO_ERRORS_SCHEMA } from "@angular/core";
import { ComponentFixture, TestBed } from "@angular/core/testing"
import { By } from "@angular/platform-browser";
import { of } from "rxjs";
import { Hero } from "../hero";
import { HeroService } from "../hero.service";
import { HeroComponent } from "../hero/hero.component";
import { HeroesComponent } from "./heroes.component"

@Directive({
    selector: '[routerLink]',
    host: { '(click)': 'onClick()' }
})
export class RouterLinkDirectiveStub {
    @Input('routerLink') linkParams: any;
    navigatedTo: any = null; 

    onClick() {
        this.navigatedTo = this.linkParams;
    }
}

describe('HeroesComponent (shallow) tests', () => {
    let fixture: ComponentFixture<HeroesComponent>;
    let mockHeroService;
    let HEROES;

    beforeEach(() => {

        HEROES = [
            { id: 1, name: 'SpiderDude', strenght: 3 },
            { id: 2, name: 'MonkeyDude', strenght: 5 },
            { id: 3, name: 'SuperDude', strenght: 12 }
        ]

        mockHeroService = jasmine.createSpyObj(["getHeroes", "addHero", "deleteHero"]);

        TestBed.configureTestingModule({
            declarations: [
                HeroesComponent,
                HeroComponent,
                RouterLinkDirectiveStub
            ],
            providers: [
                { provide: HeroService, useValue: mockHeroService }
            ],
            //schemas: [NO_ERRORS_SCHEMA]
        });

        fixture = TestBed.createComponent(HeroesComponent);
    })

    it('should render each heroes as HeroComponent', () => {
        mockHeroService.getHeroes.and.returnValue(of(HEROES));
        fixture.detectChanges();

        const heroComponentDebugElements = 
            fixture.debugElement.queryAll(By.directive(HeroComponent));

        expect(heroComponentDebugElements.length).toBe(3);
        for (let index = 0; index < HEROES.length; index++) {
            expect(heroComponentDebugElements[index].componentInstance.hero).toEqual(HEROES[index]);
        }
    })

    it(`should call heroService.deleteHero when the Hero Components
        delete button is clicked`, () => {
            mockHeroService.getHeroes.and.returnValue(of(HEROES));
            fixture.detectChanges();

            spyOn(fixture.componentInstance, "delete");
            const heroComponentDebugElements = fixture.debugElement.queryAll(By.directive(HeroComponent));
        
            expect(heroComponentDebugElements.length).toBe(3);

            //heroComponentDebugElements[0].query(By.css('button')).triggerEventHandler('click', {stopPropagation: () => {}})
            // (<HeroComponent>heroComponentDebugElements[0].componentInstance).delete.emit(undefined)
            heroComponentDebugElements[0].triggerEventHandler('delete', null);

            expect(fixture.componentInstance.delete).toHaveBeenCalledWith(HEROES[0]);
        });

    it ('should add new hero to the list when the add button is clicked', () => {
        const name = "Mr. Karniszon"
        
        mockHeroService.getHeroes.and.returnValue(of(HEROES));
        fixture.detectChanges();

        mockHeroService.addHero.and.returnValue(of({ id: 4, name: name, strenght: 4 }));

        const inputNativeElement = fixture.debugElement.query(By.css('input')).nativeElement;
        const addButtonDebugElement = fixture.debugElement.queryAll(By.css('button'))[0];

        inputNativeElement.value = name;
        addButtonDebugElement.triggerEventHandler('click', null);
        fixture.detectChanges();

        const heroesText = fixture.debugElement.query(By.css('ul')).nativeElement.textContent;
        expect(heroesText).toContain(name);
    });

    it ('should have the correct route for the first hero', () => {
        mockHeroService.getHeroes.and.returnValue(of(HEROES));
        fixture.detectChanges();

        const heroComponents = fixture.debugElement.queryAll(By.directive(HeroComponent));
        const routerLink = heroComponents[0].query(By.directive(RouterLinkDirectiveStub))
            .injector.get(RouterLinkDirectiveStub);

        heroComponents[0].query(By.css('a')).triggerEventHandler('click', null);

        expect(routerLink.linkParams).toBe('/detail/1')

    })
})