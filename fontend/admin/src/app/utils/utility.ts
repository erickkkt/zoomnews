
export class Utility {

    public static convertWords(words: string) {
        let splitStr = words.split(' ');
        for (let i = 0; i < splitStr.length; i++) {
            splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
        }
        return splitStr.join(' ');
    }

    public static isNullOrWhiteSpace(value) {
        if (value == null) return true;
        return value.replace(/^\s+|\s+$/g, "").length === 0;
    }

    public static encodeSearchString(searchString: string) {
        if (this.isNullOrWhiteSpace(searchString)) {
            return 'null';
        }
        return encodeURIComponent(searchString);
    }

    public static formatPhoneNumber(phoneNumber: string) {
        phoneNumber = phoneNumber.replace('/[^\d]/g', "").replace(' ', "");

        const regex1 = '^([0][17][0-9]{3}) ?([0-9]{5,6})$';
        let match = phoneNumber.match(regex1);
        if (match) {
            return match[1] + ' ' + match[2];
        }

        const regex2 = '^([0][238][0-9]{2}) ?([0-9]{3}) ?([0-9]{4})$';
        match = phoneNumber.match(regex2);
        if (match) {
            return match[1] + ' ' + match[2] + ' ' + match[3];
        }

        return phoneNumber;
    }
}

