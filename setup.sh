#!/bin/bash

# Função para verificar se um comando existe
command_exists() {
    command -v "$1" &> /dev/null
}

# 1- Verificar se existe MySQL instalado e, se não existir, instalar
if ! command_exists mysql; then
    echo "MySQL não está instalado. Instalando MySQL..."
    
    # Comandos para instalar MySQL (exemplo para Ubuntu/Debian)
    sudo apt-get update
    sudo apt-get install mysql-server -y
    
    # Iniciar o serviço MySQL
    sudo systemctl start mysql
    sudo systemctl enable mysql
    
    echo "MySQL instalado com sucesso."
else
    echo "MySQL já está instalado."
fi

# 2- Verificar se o .NET 6.0.0 está instalado e, se não existir, instalar, além de realizar todas as 
# configurações necessárias
if ! command_exists dotnet || ! dotnet --list-sdks | grep -q "6.0.133"; then
    echo ".NET 6.0 não está instalado. Instalando .NET 6.0.133..."
    
    # Comandos para instalar o .NET 6.0
    sudo apt-get update
    sudo apt-get install -y wget
    wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    sudo apt-get update
    sudo apt-get install -y dotnet-sdk-6.0
    
    echo ".NET 6.0 instalado com sucesso."
else
    echo ".NET 6.0 já está instalado."
fi

# Verificando o $PATH se contém o dotnet corretamente
DOTNET_TOOLS_DIR="$HOME/.dotnet/tools"
if [[ ":$PATH:" != *":$DOTNET_TOOLS_DIR:"* ]]; then
    echo "Adicionando o diretório de ferramentas do dotnet ao PATH..."
    export PATH="$PATH:$DOTNET_TOOLS_DIR"
    echo 'export PATH="$PATH:$DOTNET_TOOLS_DIR"' >> "$HOME/.bashrc"
    echo "Diretório de ferramentas do dotnet adicionado ao PATH."
else
    echo "Diretório de ferramentas do dotnet já está no PATH."
fi

# Verificando se o entity framework está instalado
if ! dotnet tool list -g | grep -q "dotnet-ef"; then
    echo "A ferramenta 'Entity Framework' não está instalada. Instalando 'dotnet-ef'..."
    dotnet tool install --global dotnet-ef --version 6.0.0
    echo "'Entity Framework' instalada com sucesso."
else
    echo "'Entity Framework' já está instalada."
fi

# 3- Entrar no MySQL e criar uma nova database
DB_NAME="jaetasks"
MYSQL_USER="root"

echo "Verificando se a database '$DB_NAME' já existe..."
DB_EXISTS=$(mysql -u $MYSQL_USER -e "SHOW DATABASES LIKE '$DB_NAME';" | grep "$DB_NAME")

if [ "$DB_EXISTS" ]; then
    echo "A database '$DB_NAME' já existe."
else
    echo "A database '$DB_NAME' não existe. Criando nova database..."
    mysql -u $MYSQL_USER -e "CREATE DATABASE $DB_NAME;"
    echo "Database '$DB_NAME' criada com sucesso."
fi
echo "Atualizando a database com o Entity Framework..."

dotnet ef database update

echo "Setup inicial finalizado."