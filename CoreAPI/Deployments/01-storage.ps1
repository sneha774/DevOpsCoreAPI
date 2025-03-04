$rg = 'devops'

New-AzResourceGroupDeployment `
    -ResourceGroupName $rg `
    -TemplateFile 'storage.dev.template.json' `
    -storageAccountName 'sindurkardevopssa2' `
    -Verbose