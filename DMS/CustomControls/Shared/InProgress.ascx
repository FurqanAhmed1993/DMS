<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InProgress.ascx.cs" Inherits="Controls_InProgress" %>

<style type="text/css">
    .loaderBody {
        position: fixed;
        top: 40%;
        left: 35%;
        z-index: 99999;
    }

    .loader {
        background-color: black;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: 9999;
        overflow: hidden;
        opacity: 0.5;
    }

    .widget {
        border-radius: 5px;
        padding: 15px 20px;
        /*margin-bottom: 10px;*/
        margin-top: 10px;
    }

        .widget.style1 h2 {
            font-size: 30px;
        }

        .widget h2,
        .widget h3 {
            margin-top: 5px;
            margin-bottom: 0;
        }

    .widget-text-box {
        padding: 20px;
        border: 1px solid #e7eaec;
        background: #ffffff;
    }

    .widget-head-color-box {
        border-radius: 5px 5px 0 0;
        margin-top: 10px;
    }

    .widget .flot-chart {
        height: 100px;
    }

    .lazur-bg {
        background-color: #3a406f;
        color: #ffffff;
    }

    .spinner {
        width: 25px;
        height: 25px;
        background-color: white;
        margin: 5px auto;
        -webkit-animation: sk-rotateplane 1.2s infinite ease-in-out;
        animation: sk-rotateplane 1.2s infinite ease-in-out;
    }

    @-webkit-keyframes sk-rotateplane {
        0% {
            -webkit-transform: perspective(120px);
        }

        50% {
            -webkit-transform: perspective(120px) rotateY(180deg);
        }

        100% {
            -webkit-transform: perspective(120px) rotateY(180deg) rotateX(180deg);
        }
    }

    @keyframes sk-rotateplane {
        0% {
            transform: perspective(120px) rotateX(0deg) rotateY(0deg);
            -webkit-transform: perspective(120px) rotateX(0deg) rotateY(0deg);
        }

        50% {
            transform: perspective(120px) rotateX(-180.1deg) rotateY(0deg);
            -webkit-transform: perspective(120px) rotateX(-180.1deg) rotateY(0deg);
        }

        100% {
            transform: perspective(120px) rotateX(-180deg) rotateY(-179.9deg);
            -webkit-transform: perspective(120px) rotateX(-180deg) rotateY(-179.9deg);
        }
    }
</style>


<div class="col-xs-4 loaderBody">
    <div class="widget lazur-bg p-lg text-center ">
        <div class="sk-spinner sk-spinner-rotating-plane" style="background-color: white"></div>
        <div class="spinner"></div>
        <span>Please wait while loading...</span>
    </div>
</div>
<div class="loader"></div>



