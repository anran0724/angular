export class NgxBootstrapLocaleMappingService {
    map(locale: string): string {
        const cultureMap = {
            'zh-Hans': 'zh-cn',
            'en': 'en-gb'
            // Add more here
        };

        if (cultureMap[locale]) {
            return cultureMap[locale];
        }

        return locale;
    }

    getModuleName(locale: string): string {
        const moduleNameMap = {
            'pt-BR': 'ptBr',
            'zh-Hans': 'zhCn',
            'en': 'enGb'
            // Add more here
        };

        if (moduleNameMap[locale]) {
            return moduleNameMap[locale];
        }

        return locale;
    }
}
