import {Pipe, PipeTransform} from "@angular/core";

@Pipe({name: 'mapToArray'})
export class MapToArray implements PipeTransform {
    transform(value: { [name: string]: string }) : any {
        let arr = [];
        for (let key in value) {
            arr.push({key: key, value: value[key]});
        }
        return arr;
    }
}