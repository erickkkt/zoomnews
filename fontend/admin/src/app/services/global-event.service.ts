import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class GlobalEventService {
    private static subscribers: { [id: string]: any; } = {};
    subscribe(name: string, func: (data: any) => void, error: (e: any) => void = {} as any): Subsciption {
        if (!name) {
            throw new Error('must provide name');
        }

        if (!func) {
            throw new Error('must provide func');
        }

        let subscriber = {};
        if (GlobalEventService.subscribers[name]) {
            subscriber = GlobalEventService.subscribers[name];
        } else {
            GlobalEventService.subscribers[name] = subscriber;
        }

        const id = name + new Date().getTime();
        subscriber[id] = { func: func, error: error };

        return new Subsciption(id, subscriber);
    }

    publish(name: string, data: any = null) {
        if (!name) {
            throw new Error('must provide name');
        }

        if (GlobalEventService.subscribers[name]) {
            const subscriber = GlobalEventService.subscribers[name];
            for (const prop in subscriber) {
                if (subscriber.hasOwnProperty(prop)) {
                    const obj = subscriber[prop];
                    try {
                        obj.func(data);
                    } catch (e) {
                        if (obj.error) {
                            obj.error(e);
                        }
                    }
                }
            }
        }
    }
}

export class Subsciption {
    constructor(private id: string, private subscriber: any) {}
    unsubscribe(): void {
        delete this.subscriber[this.id];
    }
}
