
<h1  align="center">Oralytics</h1>

  

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

  

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

  

![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)

  

![Oracle](https://img.shields.io/badge/Oracle-F80000?style=for-the-badge&logo=oracle&logoColor=white)

  

## Integrantes do Grupo

  
  

Kayque Ferreira dos Santos - Desenvolvedor Backend

  

André Alves da Silva - Desenvolvedor Backend

  

Gabriel de Souza Grego - Desenvolvedor Frontend (não atuou nesse código)

  

# Oralytics - Dental Health Monitoring Platform

  
 
  

## Video S P R I N T

  

  

[LINK PARA O VIDEO DO ENTREGAVEL DA SPRINT](https://youtu.be/_pW4yQQ2Sys)

  
  

## 🦷 Descrição

  
  

O **Oralytics** é uma plataforma voltada para o monitoramento da saúde dentária dos usuários. Utilizamos inteligência artificial para ajudar os usuários a manterem uma rotina saudável de higiene bucal, identificando problemas dentários a partir de informações de monitoramento e oferecendo orientações personalizadas baseadas no perfil de cada usuário.

  

---

  
  

## 🎯 Objetivo do Projeto

  
  

O projeto **Oralytics** tem como objetivo principal resolver problemas relacionados ao monitoramento inadequado da saúde bucal e fornecer orientações personalizadas para usuários e profissionais da área odontológica. A plataforma visa:

  

  

- Ajudar pacientes a monitorar sua saúde bucal com mais eficiência.

  

- Proporcionar a dentistas e clínicas odontológicas uma ferramenta para acompanhar o progresso dos pacientes.

  
  
  

- Identificar precocemente possíveis problemas dentários com a ajuda de IA e dados de monitoramento.

  

---

  

## 📋 Escopo

  

A aplicação **Oralytics** irá:

  

1. Coletar dados de saúde bucal dos usuários, como frequência de escovação, histórico de problemas relatados e outros parâmetros relacionados.

  
  

2. Utilizar inteligência artificial para identificar padrões que possam indicar problemas dentários.

  

3. Fornecer relatórios detalhados com base nos dados coletados, oferecendo recomendações personalizadas.

  

4. Permitir que dentistas e clínicas acompanhem o progresso dos pacientes através de dashboards personalizados.

  

5. Garantir o armazenamento seguro dos dados dos usuários.

  

**Funcionalidades principais**:

  

- Cadastro de usuários (pacientes e dentistas).

  

- Coleta de dados de higiene bucal.

  

- Geração de relatórios e recomendações personalizadas.

  

- Acompanhamento de pacientes por parte dos profissionais de saúde.

  

- Interface de monitoramento e histórico de saúde bucal.

---

---

  

## 🆚 Monolito vs Microserviços

  

Uma arquitetura monolítica, consiste em manter todos os componentes do software dentro de uma única aplicação, enquanto o microserviços tem a idéia de separar diferentes componentes em aplicações invididuais, mas mantendo uma comunicação (API por exemplo).

  

---

  

  

## 📐 Arquitetura Escolhida

  

Arquitetura escolhida é **monolítica**, pois a aplicação não irá ser muito grande, não havendo necessidade de dividi-la o pequenas partes como ocorre em microserviços. Optandor por uma arquitetura **monolítica**, irá simplificar o processo de desenvolvimento e manutenção, dado que todos os componentes estarão juntos em uma unica aplicação. Reduzindo complexidade, facilitando o gerencimaento de implantação em um ambiente _Cloud_ por exemplo, além de ser mais aderente a projetos de menor escala, como é o caso dessa solução.

  

Além disso, uma arquitetura de microserviços exigiria um gerenciamento de cada aplicação, acrescentando uma complexidade a mais para gerenciar e manter a comunicação entre os serviços independentes.

  

---

  

  

## 💡 Tecnologias Utilizadas

  

  

  

-  **Backend**: C# com ASP.NET

  

  

  

-  **Frontend**: React Native

  

  

  

-  **Banco de Dados**: Oracle

  

  

  

-  **Inteligência Artificial**: Roboflow

  

  

  

-  **Hospedagem**: Azure

  

  

 

  

---
## 🚀 Pipeline CI & CD com Azure DevOps



### Desenho Arquitetura

  

![Link imagem do desenho da arquitetura](https://raw.githubusercontent.com/WedSan/cloud-devops-challenge/refs/heads/main/asset/devops_diagram.drawio.png)
Este diagrama ilustra um pipeline de **Integração Contínua e Entrega Contínua (CI/CD)** configurado no **Azure DevOps**.  
O objetivo principal é automatizar o processo de build, teste e implantação de uma aplicação em container no **Azure Container Instance (ACI)**, garantindo agilidade e confiabilidade nas entregas.

## ⚙️ Funcionamento Geral

O fluxo se inicia com o desenvolvimento da aplicação.  
Qualquer alteração no código-fonte versionado no **Azure Repos** dispara automaticamente o pipeline no **Azure Pipelines**. Este, por sua vez, orquestra uma série de etapas:

- Download do código
- Login no Docker
- Construção da imagem da aplicação
- Envio dessa imagem para o **Azure Container Registry (ACR)**
- Criação e implantação do container no **Azure Container Instance (ACI)**

Ao final do processo, o deploy é concluído e a nova versão da aplicação está disponível ✅

---

## 📌 Passo a Passo do Pipeline

1. **Desenvolvimento e Commit**  
   O ciclo se inicia no ambiente de desenvolvimento. Após realizar as modificações no código, o desenvolvedor faz o commit para o repositório configurado no Azure Repos.

2. **Trigger na Pipeline**  
   O commit realizado serve como gatilho (trigger) para iniciar automaticamente a pipeline configurada no Azure Pipelines.

3. **Baixa Repositório**  
   A pipeline faz o checkout do código-fonte mais recente do Azure Repos para o agente de build.

4. **Docker Login**  
   A pipeline executa uma tarefa de login no **Azure Container Registry (ACR)**, autenticando o agente de build para envio da imagem Docker.

5. **Build da Imagem**  
   Com o código-fonte disponível e o login realizado, inicia-se a construção da imagem Docker da aplicação com base no `Dockerfile`.

6. **Push no ACR**  
   Após a construção da imagem, ela é enviada (push) para o ACR, onde ficará armazenada com segurança.

7. **Criação do Container com a Imagem do ACR**  
   Utilizando a imagem recém-publicada, a pipeline cria (ou atualiza) um container no **Azure Container Instance (ACI)**.

8. **Deploy Concluído**  
   Após a inicialização bem-sucedida do container no AKS, o deploy é finalizado.  
   A nova versão da aplicação está agora em execução e acessível! 

---
