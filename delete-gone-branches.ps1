# delete-gone-branches.ps1
$protectedBranches = @('main', 'master')

Write-Host "Verific branch-urile locale care nu mai exista pe origin..."

# Obține toate branch-urile locale
$localBranches = git branch --format="%(refname:short)"

foreach ($branch in $localBranches) {
    $branchName = $branch.Trim()

    # Sari peste branch-urile protejate
    if ($protectedBranches -contains $branchName) {
        continue
    }

    # Verifică dacă branch-ul mai există pe origin
    git ls-remote --exit-code --heads origin $branchName > $null 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Sterg branch-ul local '$branchName' (nu mai exista pe remote)..."
        git branch -D $branchName
    }
}

Write-Host "Proces terminat."
