﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="banner2.ascx.cs" Inherits="ProjectWeMate.banner2ascx" %>

<style>
    .aaa, .bbb, .ccc, .ddd, .eee, .fff, .ggg{
        left:0px;
        opacity:1;
        position:relative;
    }
    
</style>
<script>
    $(document).ready(function () {
        function animateee() {
            var i=0;
            for (i = 0; i < 500; i++) {
                $('.aaa').animate({ left: '-500px', opacity: 0 }, 0).delay(500);
                $('.bbb').animate({ left: '-500px', opacity: 0 }, 0).delay(500);
                $('.ccc').animate({ left: '-500px', opacity: 0 }, 0).delay(500);
                $('.ddd').animate({ left: '-500px', opacity: 0 }, 0).delay(500);
                $('.eee').animate({ left: '-500px', opacity: 0 }, 0).delay(500);
                $('.fff').animate({ left: '-500px', opacity: 0 }, 0).delay(500);
                $('.ggg').animate({ left: '-500px', opacity: 0 }, 0).delay(500);

                $('.aaa').animate({ left: '0px', opacity: 1 }, 2000).delay(3000);
                $('.bbb').animate({ left: '0px', opacity: 1 }, 1900).delay(3100);
                $('.ccc').animate({ left: '0px', opacity: 1 }, 1800).delay(3200);
                $('.ddd').animate({ left: '0px', opacity: 1 }, 1700).delay(3300);
                $('.eee').animate({ left: '0px', opacity: 1 }, 1600).delay(3400);
                $('.fff').animate({ left: '0px', opacity: 1 }, 1500).delay(3500);
                $('.ggg').animate({ left: '0px', opacity: 1 }, 1400).delay(3600);

                $('.aaa').animate({ left: '500px', opacity: 0 }, 2000).delay(500);
                $('.bbb').animate({ left: '500px', opacity: 0 }, 1900).delay(600);
                $('.ccc').animate({ left: '500px', opacity: 0 }, 1800).delay(700);
                $('.ddd').animate({ left: '500px', opacity: 0 }, 1700).delay(800);
                $('.eee').animate({ left: '500px', opacity: 0 }, 1600).delay(900);
                $('.fff').animate({ left: '500px', opacity: 0 }, 1500).delay(1000);
                $('.ggg').animate({ left: '500px', opacity: 0 }, 1400).delay(1100);
            }

            $('.aaa').animate({ left: '0px', opacity: 1 }, 2000).delay(3000);
            $('.bbb').animate({ left: '0px', opacity: 1 }, 1900).delay(3100);
            $('.ccc').animate({ left: '0px', opacity: 1 }, 1800).delay(3200);
            $('.ddd').animate({ left: '0px', opacity: 1 }, 1700).delay(3300);
            $('.eee').animate({ left: '0px', opacity: 1 }, 1600).delay(3400);
            $('.fff').animate({ left: '0px', opacity: 1 }, 1500).delay(3500);
            $('.ggg').animate({ left: '0px', opacity: 1 }, 1400).delay(3600);
               
        }
        animateee();
    });
</script>



<div id="AnimatedHeaderName">
    <span class="aaa">M</span>
    <span class="bbb">a</span>
    <span class="ccc">s</span>
    <span class="ddd">s</span>
    <span class="eee">t</span>
    <span class="fff">e</span>
    <span class="ggg">c</span>
    <span class="hhh">h</span>

</div>

