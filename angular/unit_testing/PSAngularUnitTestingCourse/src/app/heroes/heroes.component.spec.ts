import { of } from 'rxjs';
import { HeroesComponent } from './heroes.component';

describe('HeroComponent', () => {
    let component: HeroesComponent;
    let HEROES;
    let mockService;

    beforeEach(() => {
         HEROES = [
             { id: 1, name: "SpiderDude", strength: 8 },
             { id: 2, name: "Womderful woman", strength: 22 },
             { id: 3, name: "SuperDude", strength: 55 }
        ]

        mockService = jasmine.createSpyObj(['getHeroes', 'addHero', 'deleteHero']);
        component = new HeroesComponent(mockService);
    })

    describe("delete", () => {

        it('should remove the indicated hero from the hroes list', () => {
            mockService.deleteHero.and.returnValue(of(true))

            component.heroes = HEROES;

            component.delete(HEROES[2]);

            expect(component.heroes.length).toBe(2);
        })

        it('should call delete hero', () => {
            mockService.deleteHero.and.returnValue(of(true))

            component.heroes = HEROES;

            component.delete(HEROES[2]);

            expect(mockService.deleteHero).toHaveBeenCalledWith(HEROES[2]);
        })
    })
});