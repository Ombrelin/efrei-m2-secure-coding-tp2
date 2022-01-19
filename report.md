---
title: "Secure Coding TP 2"
author: "Arsène Lapostolet"
...

# Introduction

In this report I will explain and demonstrate the Cross Site Request Forgery (CSRF) attack. A CSRF attack has three components : 

- An authenticated action to perform on a website. For instance : changing account info like password and/or email to take control of the user's account
- Cookie-based browser authentication
- Predictable request parameters : the attacker must be able to guess any required parameter (ex : you don't need to provide old password to change your password)

The attacker will build a fake page to the user, using social engineering (i.e : phishing) and if the user is connected to the targeted website on his browser, the attacker site will be able to perform a malicious request to the target site using the cookie authentication token stored on the user's browser.

# Demonstration

I will demonstrate this attack using a very simple website that features simple user login, using C#, .NET 6 and ASP .NET Core MVC.

