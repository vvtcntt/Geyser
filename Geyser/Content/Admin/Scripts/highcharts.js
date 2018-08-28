﻿/*
Highcharts JS v2.2.0 (2012-02-16)

(c) 2009-2011 Torstein H?nsi

License: www.highcharts.com/license
*/
(function () {
    function N(a, b) { var c; a || (a = {}); for (c in b) a[c] = b[c]; return a } function Oa() { for (var a = 0, b = arguments, c = b.length, d = {}; a < c; a++) d[b[a++]] = b[a]; return d } function Q(a, b) { return parseInt(a, b || 10) } function yb(a) { return typeof a === "string" } function kb(a) { return typeof a === "object" } function zb(a) { return Object.prototype.toString.call(a) === "[object Array]" } function Ja(a) { return typeof a === "number" } function lb(a) { return oa.log(a) / oa.LN10 } function cb(a) { return oa.pow(10, a) } function Gb(a, b) {
        for (var c =
a.length; c--; ) if (a[c] === b) { a.splice(c, 1); break } 
    } function y(a) { return a !== ba && a !== null } function r(a, b, c) { var d, e; if (yb(b)) y(c) ? a.setAttribute(b, c) : a && a.getAttribute && (e = a.getAttribute(b)); else if (y(b) && kb(b)) for (d in b) a.setAttribute(d, b[d]); return e } function Hb(a) { return zb(a) ? a : [a] } function p() { var a = arguments, b, c, d = a.length; for (b = 0; b < d; b++) if (c = a[b], typeof c !== "undefined" && c !== null) return c } function U(a, b) { if (Sb && b && b.opacity !== ba) b.filter = "alpha(opacity=" + b.opacity * 100 + ")"; N(a.style, b) } function xa(a,
b, c, d, e) { a = V.createElement(a); b && N(a, b); e && U(a, { padding: 0, border: ga, margin: 0 }); c && U(a, c); d && d.appendChild(a); return a } function O(a, b) { var c = function () { }; c.prototype = new a; N(c.prototype, b); return c } function fc(a, b, c, d) { var e = Ba.lang, f = isNaN(b = ya(b)) ? 2 : b, b = c === void 0 ? e.decimalPoint : c, d = d === void 0 ? e.thousandsSep : d, e = a < 0 ? "-" : "", c = String(Q(a = ya(+a || 0).toFixed(f))), g = c.length > 3 ? c.length % 3 : 0; return e + (g ? c.substr(0, g) + d : "") + c.substr(g).replace(/(\d{3})(?=\d)/g, "$1" + d) + (f ? b + ya(a - c).toFixed(f).slice(2) : "") }
    function gc(a, b, c, d) { var e, c = p(c, 1); e = a / c; b || (b = [1, 2, 2.5, 5, 10], d && d.allowDecimals === !1 && (c === 1 ? b = [1, 2, 5, 10] : c <= 0.1 && (b = [1 / c]))); for (d = 0; d < b.length; d++) if (a = b[d], e <= (b[d] + (b[d + 1] || b[d])) / 2) break; a *= c; return a } function Ic(a, b) {
        var c = b || [[Ab, [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], [mb, [1, 2, 5, 10, 15, 30]], [Bb, [1, 2, 5, 10, 15, 30]], [Pa, [1, 2, 3, 4, 6, 8, 12]], [na, [1, 2]], [Ka, [1, 2]], [Xa, [1, 2, 3, 4, 6]], [za, null]], d = c[c.length - 1], e = C[d[0]], f = d[1], g; for (g = 0; g < c.length; g++) if (d = c[g], e = C[d[0]], f = d[1], c[g + 1] && a <= (e * f[f.length - 1] +
C[c[g + 1][0]]) / 2) break; e === C[za] && a < 5 * e && (f = [1, 2, 5]); e === C[za] && a < 5 * e && (f = [1, 2, 5]); c = gc(a / e, f); return { unitRange: e, count: c, unitName: d[0]}
    } function Jc(a, b, c, d) {
        var e = [], f = {}, g = Ba.global.useUTC, h, i = new Date(b), b = a.unitRange, k = a.count; i.setMilliseconds(0); b >= C[mb] && i.setSeconds(b >= C[Bb] ? 0 : k * Ta(i.getSeconds() / k)); if (b >= C[Bb]) i[oc](b >= C[Pa] ? 0 : k * Ta(i[Ib]() / k)); if (b >= C[Pa]) i[pc](b >= C[na] ? 0 : k * Ta(i[Tb]() / k)); if (b >= C[na]) i[hc](b >= C[Xa] ? 1 : k * Ta(i[Ya]() / k)); b >= C[Xa] && (i[qc](b >= C[za] ? 0 : k * Ta(i[Cb]() / k)), h = i[nb]());
        b >= C[za] && (h -= h % k, i[rc](h)); if (b === C[Ka]) i[hc](i[Ya]() - i[ic]() + p(d, 1)); d = 1; h = i[nb](); for (var j = i.getTime(), m = i[Cb](), i = i[Ya](); j < c; ) e.push(j), b === C[za] ? j = ob(h + d * k, 0) : b === C[Xa] ? j = ob(h, m + d * k) : !g && (b === C[na] || b === C[Ka]) ? j = ob(h, m, i + d * k * (b === C[na] ? 1 : 7)) : (j += b * k, b <= C[Pa] && j % C[na] === 0 && (f[j] = na)), d++; e.push(j); e.info = N(a, { higherRanks: f, totalRange: b * k }); return e
    } function sc() { this.symbol = this.color = 0 } function tc(a, b, c, d, e, f, g, h, i) {
        var k = g.x, g = g.y, i = k + c + (i ? h : -a - h), j = g - b + d + 15, m; i < 7 && (i = c + k + h); i + a > c + e && (i -=
i + a - (c + e), j = g - b + d - h, m = !0); j < d + 5 ? (j = d + 5, m && g >= j && g <= j + b && (j = g + d + h)) : j + b > d + f && (j = d + f - b - h); return { x: i, y: j}
    } function Kc(a, b) { var c = a.length, d, e; for (e = 0; e < c; e++) a[e].ss_i = e; a.sort(function (a, c) { d = b(a, c); return d === 0 ? a.ss_i - c.ss_i : d }); for (e = 0; e < c; e++) delete a[e].ss_i } function Ub(a) { for (var b = a.length, c = a[0]; b--; ) a[b] < c && (c = a[b]); return c } function Jb(a) { for (var b = a.length, c = a[0]; b--; ) a[b] > c && (c = a[b]); return c } function Kb(a) { for (var b in a) a[b] && a[b].destroy && a[b].destroy(), delete a[b] } function Vb(a) {
        pb ||
(pb = xa(db)); a && pb.appendChild(a); pb.innerHTML = ""
    } function jc(a, b) { var c = "Highcharts error #" + a + ": www.highcharts.com/errors/" + a; if (b) throw c; else ea.console && console.log(c) } function Db(a) { return parseFloat(a.toPrecision(14)) } function Lb(a, b) { Wb = p(a, b.animation) } function uc() {
        var a = Ba.global.useUTC, b = a ? "getUTC" : "get", c = a ? "setUTC" : "set"; ob = a ? Date.UTC : function (a, b, c, g, h, i) { return (new Date(a, b, p(c, 1), p(g, 0), p(h, 0), p(i, 0))).getTime() }; Ib = b + "Minutes"; Tb = b + "Hours"; ic = b + "Day"; Ya = b + "Date"; Cb = b + "Month"; nb =
b + "FullYear"; oc = c + "Minutes"; pc = c + "Hours"; hc = c + "Date"; qc = c + "Month"; rc = c + "FullYear"
    } function pa() { } function vc(a, b) {
        function c(a) {
            function b(a, c) { this.pos = a; this.type = c || ""; this.isNew = !0; c || this.addLabel() } function c(a) { if (a) this.options = a, this.Id = a.Id; return this } function d(a, b, c, e) {
                this.isNegative = b; this.options = a; this.x = c; this.stack = e; this.alignOptions = { align: a.align || (X ? b ? "left" : "right" : "center"), verticalAlign: a.verticalAlign || (X ? "middle" : b ? "bottom" : "top"), y: p(a.y, X ? 4 : b ? 14 : -6), x: p(a.x, X ? b ? -6 : 6 :
0)
                }; this.textAlign = a.textAlign || (X ? b ? "right" : "left" : "center")
            } function e() {
                var a = [], b = [], c; R = O = null; o(z.series, function (e) {
                    if (e.visible || !q.ignoreHiddenSeries) {
                        var f = e.options, g, h, i, j, k, l, m, H, n, La = f.threshold, z, o = [], v = 0; if (D && La <= 0) La = f.threshold = null; if (u) f = e.xData, f.length && (R = Za(p(R, f[0]), Ub(f)), O = $(p(O, f[0]), Jb(f))); else {
                            var F, t, Mb, P = e.cropped, x = e.xAxis.getExtremes(), w = !!e.modifyValue; g = f.stacking; Ca = g === "percent"; if (g) k = f.stack, j = e.type + p(k, ""), l = "-" + j, e.stackKey = j, h = a[j] || [], a[j] = h, i = b[l] ||
[], b[l] = i; Ca && (R = 0, O = 99); f = e.processedXData; m = e.processedYData; z = m.length; for (c = 0; c < z; c++) if (H = f[c], n = m[c], n !== null && n !== ba && (g ? (t = (F = n < La) ? i : h, Mb = F ? l : j, n = t[H] = y(t[H]) ? t[H] + n : n, ca[Mb] || (ca[Mb] = {}), ca[Mb][H] || (ca[Mb][H] = new d(s.stackLabels, F, H, k)), ca[Mb][H].setTotal(n)) : w && (n = e.modifyValue(n)), P || (f[c + 1] || H) >= x.min && (f[c - 1] || H) <= x.max)) if (H = n.length) for (; H--; ) n[H] !== null && (o[v++] = n[H]); else o[v++] = n; !Ca && o.length && (R = Za(p(R, o[0]), Ub(o)), O = $(p(O, o[0]), Jb(o))); La !== null && (R >= La ? (R = La, Oa = !0) : O < La && (O =
La, Pa = !0))
                        } 
                    } 
                })
            } function f(a, b, c) { for (var d, b = Db(Ta(b / a) * a), c = Db(ac(c / a) * a), e = []; b <= c; ) { e.push(b); b = Db(b + a); if (b === d) break; d = b } return e } function g(a, b, c, d) {
                var e = []; if (!d) z._minorAutoInterval = null; if (a >= 0.5) a = B(a), e = f(a, b, c); else if (a >= 0.08) { var h = Ta(b), i, j, k, l, m, H; for (i = a > 0.3 ? [1, 2, 4] : a > 0.15 ? [1, 2, 4, 6, 8] : [1, 2, 3, 4, 5, 6, 7, 8, 9]; h < c + 1 && !H; h++) { k = i.length; for (j = 0; j < k && !H; j++) l = lb(cb(h) * i[j]), l > b && e.push(m), m > c && (H = !0), m = l } } else if (b = cb(b), c = cb(c), a = s[d ? "minorTickInterval" : "tickInterval"], a = p(a === "auto" ? null :
a, z._minorAutoInterval, (c - b) * (s.tickPixelInterval / (d ? 5 : 1)) / ((d ? T / W.length : T) || 1)), a = gc(a, null, oa.pow(10, Ta(oa.log(a) / oa.LN10))), e = Xb(f(a, b, c), lb), !d) z._minorAutoInterval = a / 5; d || (Ma = a); return e
            } function h() { var a = [], b, c; if (D) { c = W.length; for (b = 1; b < c; b++) a = a.concat(g(Fa, W[b - 1], W[b], !0)) } else for (b = I + (W[0] - I) % Fa; b <= J; b += Fa) a.push(b); return a } function i() {
                var a, b = O - R >= fb, c, d, e, f, g, h; u && fb === ba && !D && (y(s.min) || y(s.max) ? fb = null : (o(z.series, function (a) {
                    f = a.xData; for (d = g = a.xIncrement ? 1 : f.length - 1; d > 0; d--) if (e =
f[d] - f[d - 1], c === ba || e < c) c = e
                }), fb = Za(c * 5, O - R))); J - I < fb && (a = (fb - J + I) / 2, a = [I - a, p(s.min, I - a)], b && (a[2] = R), I = Jb(a), h = [I + fb, p(s.max, I + fb)], b && (h[2] = O), J = Ub(h), J - I < fb && (a[0] = J - fb, a[1] = p(s.min, J - fb), I = Jb(a)))
            } function j(a) {
                var b, c = s.tickInterval, d = s.tickPixelInterval; ga ? (pa = l[u ? "xAxis" : "yAxis"][s.linkedTo], b = pa.getExtremes(), I = p(b.min, b.dataMin), J = p(b.max, b.dataMax), s.type !== pa.options.type && jc(11, 1)) : (I = p(da, s.min, R), J = p(ea, s.max, O)); D && (!a && I <= 0 && jc(10), I = lb(I), J = lb(J)); ka && (da = I = $(I, J - ka), ea = J, a && (ka =
null)); i(); if (!Ua && !Ca && !ga && y(I) && y(J)) { b = J - I || 1; if (!y(s.min) && !y(da) && Ea && (R < 0 || !Oa)) I -= b * Ea; if (!y(s.max) && !y(ea) && Ga && (O > 0 || !Pa)) J += b * Ga } Ma = I === J || I === void 0 || J === void 0 ? 1 : ga && !c && d === pa.options.tickPixelInterval ? pa.tickInterval : p(c, Ua ? 1 : (J - I) * d / (T || 1)); u && !a && o(z.series, function (a) { a.processData(I !== na || J !== za) }); Y(); z.beforeSetTickPositions && z.beforeSetTickPositions(); z.postProcessTickInterval && (Ma = z.postProcessTickInterval(Ma)); !w && !D && (Wa = oa.pow(10, Ta(oa.log(Ma) / oa.LN10)), y(s.tickInterval) ||
(Ma = gc(Ma, null, Wa, s))); z.tickInterval = Ma; Fa = s.minorTickInterval === "auto" && Ma ? Ma / 5 : s.minorTickInterval; (W = s.tickPositions || Xa && Xa.apply(z, [I, J])) || (W = w ? (z.getNonLinearTimeTicks || Jc)(Ic(Ma, s.units), I, J, s.startOfWeek, z.ordinalPositions, z.closestPointRange, !0) : D ? g(Ma, I, J) : f(Ma, I, J)); Z(z, "afterSetTickPositions", { tickPositions: W }); if (!ga && (a = W[0], c = W[W.length - 1], s.startOnTick ? I = a : I > a && W.shift(), s.endOnTick ? J = c : J < c && W.pop(), hb || (hb = { x: 0, y: 0 }), !w && W.length > hb[Sa] && s.alignTicks !== !1)) hb[Sa] = W.length
            } function k(a) {
                a =
(new c(a)).render(); ra.push(a); return a
            } function m() {
                var a = s.title, d = s.stackLabels, e = s.alternateGridColor, f = s.lineWidth, g, i, j = l.hasRendered && y(na) && !isNaN(na), H = (g = z.series.length && y(I) && y(J)) || p(s.showEmpty, !0), n, q; if (g || ga) if (Fa && !Ua && o(h(), function (a) { wa[a] || (wa[a] = new b(a, "minor")); j && wa[a].isNew && wa[a].render(null, !0); wa[a].isActive = !0; wa[a].render() }), o(W, function (a, c) { if (!ga || a >= I && a <= J) Qa[a] || (Qa[a] = new b(a)), j && Qa[a].isNew && Qa[a].render(c, !0), Qa[a].isActive = !0, Qa[a].render(c) }), e && o(W, function (a,
b) { if (b % 2 === 0 && a < J) Aa[a] || (Aa[a] = new c), n = a, q = W[b + 1] !== ba ? W[b + 1] : J, Aa[a].options = { from: D ? cb(n) : n, to: D ? cb(q) : q, color: e }, Aa[a].render(), Aa[a].isActive = !0 }), !z._addedPlotLB) o((s.plotLines || []).concat(s.plotBands || []), function (a) { k(a) }), z._addedPlotLB = !0; o([Qa, wa, Aa], function (a) { for (var b in a) a[b].isActive ? a[b].isActive = !1 : (a[b].destroy(), delete a[b]) }); f && (g = r + (F ? qb : 0) + E, i = ta - Nb - (F ? ib : 0) + E, g = K.crispLine([ua, P ? r : g, P ? i : jb, ha, P ? qa - bc : g, P ? i : ta - Nb], f), aa ? aa.animate({ d: g }) : aa = K.path(g).attr({ stroke: s.lineColor,
    "stroke-width": f, zIndex: 7
}).add(), aa[H ? "show" : "hide"]()); if (v && H) H = P ? r : jb, f = Q(a.style.fontSize || 12), H = { low: H + (P ? 0 : T), middle: H + T / 2, high: H + (P ? T : 0)}[a.align], f = (P ? jb + ib : r) + (P ? 1 : -1) * (F ? -1 : 1) * Ya + (t === 2 ? f : 0), v[v.isNew ? "attr" : "animate"]({ x: P ? H : f + (F ? qb : 0) + E + (a.x || 0), y: P ? f - (F ? ib : 0) + E : H + (a.y || 0) }), v.isNew = !1; if (d && d.enabled) { var u, x, d = z.stackTotalGroup; if (!d) z.stackTotalGroup = d = K.g("stack-labels").attr({ visibility: eb, zIndex: 6 }).translate(S, L).add(); for (u in ca) for (x in a = ca[u], a) a[x].render(d) } z.isDirty = !1
            }
            function n(a) { for (var b = ra.length; b--; ) ra[b].Id === a && ra[b].destroy() } var u = a.isX, F = a.opposite, P = X ? !u : u, t = P ? F ? 0 : 2 : F ? 1 : 3, ca = {}, s = M(u ? cc : kc, [Lc, Mc, wc, Nc][t], a), z = this, v, x = s.type, w = x === "datetime", D = x === "logarithmic", E = s.offset || 0, Sa = u ? "x" : "y", T = 0, A, G, C, V, r, jb, qb, ib, Nb, bc, U, Y, gb, Rb, fa, aa, R, O, fb = s.minRange || s.maxZoom, ka = s.range, da, ea, xa, Ba, J = null, I = null, na, za, Ea = s.minPadding, Ga = s.maxPadding, Ha = 0, ga = y(s.linkedTo), pa, Oa, Pa, Ca, x = s.events, Ra, ra = [], Ma, Fa, Wa, W, Xa = s.tickPositioner, Qa = {}, wa = {}, Aa = {}, Ja, Ka, Ya, Ua =
s.categories, db = s.labels.formatter || function () { var a = this.value, b = this.dateTimeLabelFormat; return b ? dc(b, a) : Ma % 1E6 === 0 ? a / 1E6 + "M" : Ma % 1E3 === 0 ? a / 1E3 + "k" : !Ua && a >= 1E3 ? fc(a, 0) : a }, Va = P && s.labels.staggerLines, Da = s.reversed, Ia = Ua && s.tickmarkPlacement === "between" ? 0.5 : 0; b.prototype = { addLabel: function () {
    var a = this.pos, b = s.labels, c = Ua && P && Ua.length && !b.step && !b.staggerLines && !b.rotation && la / Ua.length || !P && la / 2, d = a === W[0], e = a === W[W.length - 1], f = Ua && y(Ua[a]) ? Ua[a] : a, g = this.label, h = W.info, i; w && h && (i = s.dateTimeLabelFormats[h.higherRanks[a] ||
h.unitName]); this.isFirst = d; this.isLast = e; a = db.call({ axis: z, chart: l, isFirst: d, isLast: e, dateTimeLabelFormat: i, value: D ? Db(cb(f)) : f }); c = c && { width: $(1, B(c - 2 * (b.padding || 10))) + ia }; c = N(c, b.style); y(g) ? g && g.attr({ text: a }).css(c) : this.label = y(a) && b.enabled ? K.text(a, 0, 0, b.useHTML).attr({ align: b.align, rotation: b.rotation }).css(c).add(Rb) : null
}, getLabelSize: function () { var a = this.label; return a ? (this.labelBBox = a.getBBox())[P ? "height" : "width"] : 0 }, render: function (a, b) {
    var c = this.type, d = this.label, e = this.pos, f =
s.labels, g = this.gridLine, h = c ? c + "Grid" : "grid", i = c ? c + "Tick" : "tick", j = s[h + "LineWidth"], k = s[h + "LineColor"], l = s[h + "LineDashStyle"], H = s[i + "Length"], h = s[i + "Width"] || 0, m = s[i + "Color"], n = s[i + "Position"], i = this.mark, La = f.step, ca = b && ab || ta, q; q = P ? U(e + Ia, null, null, b) + C : r + E + (F ? (b && bb || qa) - bc - r : 0); ca = P ? ca - Nb + E - (F ? ib : 0) : ca - U(e + Ia, null, null, b) - C; if (j) { e = gb(e + Ia, j, b); if (g === ba) { g = { stroke: k, "stroke-width": j }; if (l) g.dashstyle = l; if (!c) g.zIndex = 1; this.gridLine = g = j ? K.path(e).attr(g).add(fa) : null } !b && g && e && g.animate({ d: e }) } if (h) n ===
"inside" && (H = -H), F && (H = -H), c = K.crispLine([ua, q, ca, ha, q + (P ? 0 : -H), ca + (P ? H : 0)], h), i ? i.animate({ d: c }) : this.mark = K.path(c).attr({ stroke: m, "stroke-width": h }).add(Rb); d && !isNaN(q) && (q = q + f.x - (Ia && P ? Ia * G * (Da ? -1 : 1) : 0), ca = ca + f.y - (Ia && !P ? Ia * G * (Da ? 1 : -1) : 0), y(f.y) || (ca += Q(d.styles.lineHeight) * 0.9 - d.getBBox().height / 2), Va && (ca += a / (La || 1) % Va * 16), this.isFirst && !p(s.showFirstLabel, 1) || this.isLast && !p(s.showLastLabel, 1) ? d.hide() : d.show(), La && a % La && d.hide(), d[this.isNew ? "attr" : "animate"]({ x: q, y: ca })); this.isNew = !1
},
    destroy: function () { Kb(this) } 
}; c.prototype = { render: function () {
    var a = this, b = (z.pointRange || 0) / 2, c = a.options, d = c.label, e = a.label, f = c.width, g = c.to, h = c.from, i = c.value, j, k = c.dashStyle, H = a.svgElem, l = [], m, s, n = c.color; s = c.zIndex; var La = c.events; D && (h = lb(h), g = lb(g), i = lb(i)); if (f) { if (l = gb(i, f), b = { stroke: n, "stroke-width": f }, k) b.dashstyle = k } else if (y(h) && y(g)) h = $(h, I - b), g = Za(g, J + b), j = gb(g), (l = gb(h)) && j ? l.push(j[4], j[5], j[1], j[2]) : l = null, b = { fill: n }; else return; if (y(s)) b.zIndex = s; if (H) l ? H.animate({ d: l }, null, H.onGetPath) :
(H.hide(), H.onGetPath = function () { H.show() }); else if (l && l.length && (a.svgElem = H = K.path(l).attr(b).add(), La)) for (m in k = function (b) { H.on(b, function (c) { La[b].apply(a, [c]) }) }, La) k(m); if (d && y(d.text) && l && l.length && qb > 0 && ib > 0) {
        d = M({ align: P && j && "center", x: P ? !j && 4 : 10, verticalAlign: !P && j && "middle", y: P ? j ? 16 : 10 : j ? 6 : -4, rotation: P && !j && 90 }, d); if (!e) a.label = e = K.text(d.text, 0, 0).attr({ align: d.textAlign || d.align, rotation: d.rotation, zIndex: s }).css(d.style).add(); j = [l[1], l[4], p(l[6], l[1])]; l = [l[2], l[5], p(l[7], l[2])];
        m = Ub(j); s = Ub(l); e.align(d, !1, { x: m, y: s, width: Jb(j) - m, height: Jb(l) - s }); e.show()
    } else e && e.hide(); return a
}, destroy: function () { Kb(this); Gb(ra, this) } 
}; d.prototype = { destroy: function () { Kb(this) }, setTotal: function (a) { this.cum = this.total = a }, render: function (a) { var b = this.options.formatter.call(this); this.label ? this.label.attr({ text: b, visibility: $a }) : this.label = l.renderer.text(b, 0, 0).css(this.options.style).attr({ align: this.textAlign, rotation: this.options.rotation, visibility: $a }).add(a) }, setOffset: function (a,
b) { var c = this.isNegative, d = z.translate(this.total, 0, 0, 0, 1), e = z.translate(0), e = ya(d - e), f = l.xAxis[0].translate(this.x) + a, g = l.plotHeight, c = { x: X ? c ? d : d - e : f, y: X ? g - f - b : c ? g - d - e : g - d, width: X ? e : b, height: X ? b : e }; this.label && this.label.align(this.alignOptions, null, c).attr({ visibility: eb }) } 
}; U = function (a, b, c, d, e) { var f = 1, g = 0, h = d ? V : G, d = d ? na : I, e = s.ordinal || D && e; h || (h = G); c && (f *= -1, g = T); Da && (f *= -1, g -= f * T); b ? (Da && (a = T - a), a = a / h + d, e && (a = z.lin2val(a))) : (e && (a = z.val2lin(a)), a = f * (a - d) * h + g + f * Ha); return a }; gb = function (a, b,
c) { var d, e, f, a = U(a, null, null, c), g = c && ab || ta, h = c && bb || qa, i, c = e = B(a + C); d = f = B(g - a - C); if (isNaN(a)) i = !0; else if (P) { if (d = jb, f = g - Nb, c < r || c > r + qb) i = !0 } else if (c = r, e = h - bc, d < jb || d > jb + ib) i = !0; return i ? null : K.crispLine([ua, c, d, ha, e, f], b || 0) }; Y = function () { var a = J - I, b = 0, c, d; if (u) ga ? b = pa.pointRange : o(z.series, function (a) { b = $(b, a.pointRange); d = a.closestPointRange; !a.noSharedTooltip && y(d) && (c = y(c) ? Za(c, d) : d) }), z.pointRange = b, z.closestPointRange = c; V = G; z.translationSlope = G = T / (a + b || 1); C = P ? r : Nb; Ha = G * (b / 2) }; va.push(z);
            l[u ? "xAxis" : "yAxis"].push(z); X && u && Da === ba && (Da = !0); N(z, { addPlotBand: k, addPlotLine: k, adjustTickAmount: function () { if (hb && hb[Sa] && !w && !Ua && !ga && s.alignTicks !== !1) { var a = Ja, b = W.length; Ja = hb[Sa]; if (b < Ja) { for (; W.length < Ja; ) W.push(Db(W[W.length - 1] + Ma)); G *= (b - 1) / (Ja - 1); J = W[W.length - 1] } if (y(a) && Ja !== a) z.isDirty = !0 } }, categories: Ua, getExtremes: function () { return { min: D ? Db(cb(I)) : I, max: D ? Db(cb(J)) : J, dataMin: R, dataMax: O, userMin: da, userMax: ea} }, getPlotLinePath: gb, getThreshold: function (a) {
                var b = D ? cb(I) : I, c = D ? cb(J) :
J; b > a || a === null ? a = b : c < a && (a = c); return U(a, 0, 1, 0, 1)
            }, isXAxis: u, options: s, plotLinesAndBands: ra, getOffset: function () {
                var a = z.series.length && y(I) && y(J), c = a || p(s.showEmpty, !0), d = 0, e = 0, f = s.title, g = s.labels, h = [-1, 1, 1, -1][t], i; Rb || (Rb = K.g("axis").attr({ zIndex: 7 }).add(), fa = K.g("grid").attr({ zIndex: s.gridZIndex || 1 }).add()); Ka = 0; if (a || ga) o(W, function (a) { Qa[a] ? Qa[a].addLabel() : Qa[a] = new b(a) }), o(W, function (a) { if (t === 0 || t === 2 || { 1: "left", 3: "right"}[t] === g.align) Ka = $(Qa[a].getLabelSize(), Ka) }), Va && (Ka += (Va - 1) * 16);
                else for (i in Qa) Qa[i].destroy(), delete Qa[i]; if (f && f.text) { if (!v) v = z.axisTitle = K.text(f.text, 0, 0, f.useHTML).attr({ zIndex: 7, rotation: f.rotation || 0, align: f.textAlign || { low: "left", middle: "center", high: "right"}[f.align] }).css(f.style).add(), v.isNew = !0; c && (d = v.getBBox()[P ? "height" : "width"], e = p(f.margin, P ? 5 : 10)); v[c ? "show" : "hide"]() } E = h * p(s.offset, sa[t]); Ya = p(f.offset, Ka + e + (t !== 2 && Ka && h * s.labels[P ? "y" : "x"])); sa[t] = $(sa[t], Ya + d + h * E)
            }, render: m, setAxisSize: function () {
                var a = s.offsetLeft || 0, b = s.offsetRight ||
0; r = p(s.left, S + a); jb = p(s.top, L); qb = p(s.width, la - a + b); ib = p(s.height, ma); Nb = ta - ib - jb; bc = qa - qb - r; T = P ? qb : ib; z.left = r; z.top = jb; z.len = T
            }, setAxisTranslation: Y, setCategories: function (b, c) { z.categories = a.categories = Ua = b; o(z.series, function (a) { a.translate(); a.setTooltipPoints(!0) }); z.isDirty = !0; p(c, !0) && l.redraw() }, setExtremes: function (a, b, c, d) { c = p(c, !0); Z(z, "setExtremes", { min: a, max: b }, function () { da = a; ea = b; c && l.redraw(d) }) }, setScale: function () {
                var a, b, c; na = I; za = J; A = T; T = P ? qb : ib; o(z.series, function (a) {
                    if (a.isDirtyData ||
a.isDirty || a.xAxis.isDirty) c = !0
                }); if (T !== A || c || ga || da !== xa || ea !== Ba) { e(); j(); xa = da; Ba = ea; if (!u) for (a in ca) for (b in ca[a]) ca[a][b].cum = ca[a][b].total; if (!z.isDirty) z.isDirty = l.isDirtyBox || I !== na || J !== za } 
            }, setTickPositions: j, translate: U, redraw: function () { rb.resetTracker && rb.resetTracker(); m(); o(ra, function (a) { a.render() }); o(z.series, function (a) { a.isDirty = !0 }) }, removePlotBand: n, removePlotLine: n, reversed: Da, setTitle: function (a, b) { s.title = M(s.title, a); v = v.destroy(); z.isDirty = !0; p(b, !0) && l.redraw() }, series: [],
                stacks: ca, destroy: function () { var a; Na(z); for (a in ca) Kb(ca[a]), ca[a] = null; if (z.stackTotalGroup) z.stackTotalGroup = z.stackTotalGroup.destroy(); o([Qa, wa, Aa, ra], function (a) { Kb(a) }); o([aa, Rb, fa, v], function (a) { a && a.destroy() }); aa = Rb = fa = v = null } 
            }); for (Ra in x) ja(z, Ra, x[Ra]); if (D) z.val2lin = lb, z.lin2val = cb
        } function d(a) {
            function b() {
                var c = this.points || Hb(this), d = c[0].series, e; e = [d.tooltipHeaderFormatter(c[0].key)]; o(c, function (a) { d = a.series; e.push(d.tooltipFormatter && d.tooltipFormatter(a) || a.point.tooltipFormatter(d.tooltipOptions.pointFormat)) });
                e.push(a.footerFormat || ""); return e.join("")
            } function c(a, b) { n = m ? a : (2 * n + a) / 3; q = m ? b : (q + b) / 2; u.attr({ x: n, y: q }); mb = ya(a - n) > 1 || ya(b - q) > 1 ? function () { c(a, b) } : null } function d() { if (!m) { var a = l.hoverPoints; u.hide(); a && o(a, function (a) { a.setState() }); l.hoverPoints = null; m = !0 } } var e, f = a.borderWidth, g = a.crosshairs, h = [], i = a.style, j = a.shared, k = Q(i.padding), m = !0, n = 0, q = 0; i.padding = 0; var u = K.label("", 0, 0, null, null, null, a.useHTML).attr({ padding: k, fill: a.backgroundColor, "stroke-width": f, r: a.borderRadius, zIndex: 8 }).css(i).hide().add();
            wa || u.shadow(a.shadow); return { shared: j, refresh: function (f) {
                var i, k, n, q, v = {}, t = []; n = f.tooltipPos; i = a.formatter || b; var v = l.hoverPoints, F; j && (!f.series || !f.series.noSharedTooltip) ? (q = 0, v && o(v, function (a) { a.setState() }), l.hoverPoints = f, o(f, function (a) { a.setState(Ra); q += a.plotY; t.push(a.getLabelConfig()) }), k = f[0].plotX, q = B(q) / f.length, v = { x: f[0].category }, v.points = t, f = f[0]) : v = f.getLabelConfig(); v = i.call(v); e = f.series; k = p(k, f.plotX); q = p(q, f.plotY); i = B(n ? n[0] : X ? la - q : k); k = B(n ? n[1] : X ? ma - k : q); n = j || !e.isCartesian ||
e.tooltipOutsidePlot || Eb(i, k); v === !1 || !n ? d() : (m && (u.show(), m = !1), u.attr({ text: v }), F = a.borderColor || f.color || e.color || "#606060", u.attr({ stroke: F }), n = tc(u.width, u.height, S, L, la, ma, { x: i, y: k }, p(a.distance, 12), X), c(B(n.x), B(n.y))); if (g) {
                    g = Hb(g); var x; n = g.length; for (var D; n--; ) if (x = f.series[n ? "yAxis" : "xAxis"], g[n] && x) if (x = x.getPlotLinePath(f[n ? "y" : "x"], 1), h[n]) h[n].attr({ d: x, visibility: eb }); else {
                        D = { "stroke-width": g[n].width || 1, stroke: g[n].color || "#C0C0C0", zIndex: g[n].zIndex || 2 }; if (g[n].dashStyle) D.dashstyle =
g[n].dashStyle; h[n] = K.path(x).attr(D).add()
                    } 
                } Z(l, "tooltipRefresh", { text: v, x: i + S, y: k + L, borderColor: F })
            }, hide: d, hideCrosshairs: function () { o(h, function (a) { a && a.hide() }) }, destroy: function () { o(h, function (a) { a && a.destroy() }); u && (u = u.destroy()) } 
            }
        } function e(a) {
            function b(a) {
                var c, d, e, a = a || ea.event; if (!a.target) a.target = a.srcElement; if (a.originalEvent) a = a.originalEvent; if (a.event) a = a.event; c = a.touches ? a.touches.item(0) : a; Va = xc(G); d = Va.left; e = Va.top; Sb ? (d = a.x, c = a.y) : (d = c.pageX - d, c = c.pageY - e); return N(a, { chartX: B(d),
                    chartY: B(c)
                })
            } function c(a) { var b = { xAxis: [], yAxis: [] }; o(va, function (c) { var d = c.translate, e = c.isXAxis; b[e ? "xAxis" : "yAxis"].push({ axis: c, value: d((X ? !e : e) ? a.chartX - S : ma - a.chartY + L, !0) }) }); return b } function e() { var a = l.hoverSeries, b = l.hoverPoint; if (b) b.onMouseOut(); if (a) a.onMouseOut(); sb && (sb.hide(), sb.hideCrosshairs()); nb = null } function f() {
                if (n) {
                    var a = { xAxis: [], yAxis: [] }, b = n.getBBox(), c = b.x - S, d = b.y - L; k && (o(va, function (e) {
                        if (e.options.zoomEnabled !== !1) {
                            var f = e.translate, g = e.isXAxis, h = X ? !g : g, i = f(h ? c : ma -
d - b.height, !0, 0, 0, 1), f = f(h ? c + b.width : ma - d, !0, 0, 0, 1); a[g ? "xAxis" : "yAxis"].push({ axis: e, min: Za(i, f), max: $(i, f) })
                        } 
                    }), Z(l, "selection", a, Ab)); n = n.destroy()
                } U(G, { cursor: "auto" }); l.mouseIsDown = Ya = k = !1; Na(V, ra ? "touchend" : "mouseup", f)
            } function g(a) { var b = y(a.pageX) ? a.pageX : a.page.x, a = y(a.pageX) ? a.pageY : a.page.y; Va && !Eb(b - Va.left - S, a - Va.top - L) && e() } function h() { e(); Va = null } var i, j, k, n, m = wa ? "" : q.zoomType, u = /x/.test(m), v = /y/.test(m), F = u && !X || v && X, t = v && !X || u && X; Aa = function () {
                Wa ? (Wa.translate(S, L), X && Wa.attr({ width: l.plotWidth,
                    height: l.plotHeight
                }).invert()) : l.trackerGroup = Wa = K.g("tracker").attr({ zIndex: 9 }).add()
            }; Aa(); if (a.enabled) l.tooltip = sb = d(a), Cb = setInterval(function () { mb && mb() }, 32); (function () {
                G.onmousedown = function (a) { a = b(a); !ra && a.preventDefault && a.preventDefault(); l.mouseIsDown = Ya = !0; l.mouseDownX = i = a.chartX; j = a.chartY; ja(V, ra ? "touchend" : "mouseup", f) }; var d = function (c) {
                    if (!c || !(c.touches && c.touches.length > 1)) {
                        c = b(c); if (!ra) c.returnValue = !1; var d = c.chartX, e = c.chartY, f = !Eb(d - S, e - L); ra && c.type === "touchstart" && (r(c.target,
"isTracker") ? l.runTrackerClick || c.preventDefault() : !Bb && !f && c.preventDefault()); f && (d < S ? d = S : d > S + la && (d = S + la), e < L ? e = L : e > L + ma && (e = L + ma)); if (Ya && c.type !== "touchstart") { if (k = Math.sqrt(Math.pow(i - d, 2) + Math.pow(j - e, 2)), k > 10) { var g = Eb(i - S, j - L); if (Ob && (u || v) && g) n || (n = K.rect(S, L, F ? 1 : la, t ? 1 : ma, 0).attr({ fill: q.selectionMarkerFill || "rgba(69,114,167,0.25)", zIndex: 7 }).add()); n && F && (c = d - i, n.attr({ width: ya(c), x: (c > 0 ? 0 : c) + i })); n && t && (e -= j, n.attr({ height: ya(e), y: (e > 0 ? 0 : e) + j })); g && !n && q.panning && l.pan(d) } } else if (!f) {
                            var h,
d = l.hoverPoint, e = l.hoverSeries, m, o, g = qa, x = X ? c.chartY : c.chartX - S; if (sb && a.shared && (!e || !e.noSharedTooltip)) { h = []; m = Y.length; for (o = 0; o < m; o++) if (Y[o].visible && Y[o].options.enableMouseTracking !== !1 && !Y[o].noSharedTooltip && Y[o].tooltipPoints.length) c = Y[o].tooltipPoints[x], c._dist = ya(x - c.plotX), g = Za(g, c._dist), h.push(c); for (m = h.length; m--; ) h[m]._dist > g && h.splice(m, 1); if (h.length && h[0].plotX !== nb) sb.refresh(h), nb = h[0].plotX } if (e && e.tracker && (c = e.tooltipPoints[x]) && c !== d) c.onMouseOver()
                        } return f || !Ob
                    } 
                }; G.onmousemove =
d; ja(G, "mouseleave", h); ja(V, "mousemove", g); G.ontouchstart = function (a) { if (u || v) G.onmousedown(a); d(a) }; G.ontouchmove = d; G.ontouchend = function () { k && e() }; G.onclick = function (a) { var d = l.hoverPoint, a = b(a); a.cancelBubble = !0; if (!k) if (d && r(a.target, "isTracker")) { var e = d.plotX, f = d.plotY; N(d, { pageX: Va.left + S + (X ? la - f : e), pageY: Va.top + L + (X ? ma - e : f) }); Z(d.series, "click", N(a, { point: d })); d.firePointEvent("click", a) } else N(a, c(a)), Eb(a.chartX - S, a.chartY - L) && Z(l, "click", a); k = !1 } 
            })(); N(this, { zoomX: u, zoomY: v, resetTracker: e,
                normalizeMouseEvent: b, destroy: function () { if (l.trackerGroup) l.trackerGroup = Wa = l.trackerGroup.destroy(); Na(G, "mouseleave", h); Na(V, "mousemove", g); G.onclick = G.onmousedown = G.onmousemove = G.ontouchstart = G.ontouchend = G.ontouchmove = null } 
            })
        } function f(a) { var b = a.type || q.type || q.defaultSeriesType, c = Ca[b], d = l.hasRendered; if (d) if (X && b === "column") c = Ca.bar; else if (!X && b === "bar") c = Ca.column; b = new c; b.init(l, a); !d && b.inverted && (X = !0); if (b.isCartesian) Ob = b.isCartesian; Y.push(b); return b } function g() {
            q.alignTicks !==
!1 && o(va, function (a) { a.adjustTickAmount() }); hb = null
        } function h(a) {
            var b = l.isDirtyLegend, c, d = l.isDirtyBox, e = Y.length, f = e, h = l.clipRect; for (Lb(a, l); f--; ) if (a = Y[f], a.isDirty && a.options.stacking) { c = !0; break } if (c) for (f = e; f--; ) if (a = Y[f], a.options.stacking) a.isDirty = !0; o(Y, function (a) { a.isDirty && a.options.legendType === "point" && (b = !0) }); if (b && Yb.renderLegend) Yb.renderLegend(), l.isDirtyLegend = !1; Ob && (Ja || (hb = null, o(va, function (a) { a.setScale() })), g(), Zb(), o(va, function (a) {
                Z(a, "afterSetExtremes", a.getExtremes());
                a.isDirty && a.redraw()
            })); d && (ob(), Aa(), h && (Pb(h), h.animate({ width: l.plotSizeX, height: l.plotSizeY + 1 }))); o(Y, function (a) { a.isDirty && a.visible && (!a.isCartesian || a.xAxis) && a.redraw() }); rb && rb.resetTracker && rb.resetTracker(); K.draw(); Z(l, "redraw")
        } function i() { var b = a.xAxis || {}, d = a.yAxis || {}, b = Hb(b); o(b, function (a, b) { a.index = b; a.isX = !0 }); d = Hb(d); o(d, function (a, b) { a.index = b }); b = b.concat(d); o(b, function (a) { new c(a) }); g() } function k() {
            var a = Ba.lang, b = q.resetZoomButton, c = b.theme, d = c.states, e = b.relativeTo ===
"chart" ? null : { x: S, y: L, width: la, height: ma }; l.resetZoomButton = K.button(a.resetZoom, null, null, Ib, c, d && d.hover).attr({ align: b.position.align, title: a.resetZoomTitle }).add().align(b.position, !1, e)
        } function j(b, c) { fa = M(a.title, b); da = M(a.subtitle, c); o([["title", b, fa], ["subtitle", c, da]], function (a) { var b = a[0], c = l[b], d = a[1], a = a[2]; c && d && (c = c.destroy()); a && a.text && !c && (l[b] = K.text(a.text, 0, 0, a.useHTML).attr({ align: a.align, "class": Da + b, zIndex: 1 }).css(a.style).add().align(a, !1, R)) }) } function m() {
            Ea = q.renderTo;
            Oa = Da + lc++; yb(Ea) && (Ea = V.getElementById(Ea)); Ea || jc(13, !0); Ea.innerHTML = ""; Ea.offsetWidth || (aa = Ea.cloneNode(0), U(aa, { position: tb, top: "-9999px", display: "" }), V.body.appendChild(aa)); na = (aa || Ea).offsetWidth; za = (aa || Ea).offsetHeight; l.chartWidth = qa = q.width || na || 600; l.chartHeight = ta = q.height || (za > 19 ? za : 400); l.container = G = xa(db, { className: Da + "container" + (q.className ? " " + q.className : ""), id: Oa }, N({ position: yc, overflow: $a, width: qa + ia, height: ta + ia, textAlign: "left", lineHeight: "normal" }, q.style), aa || Ea); l.renderer =
K = q.forExport ? new Fb(G, qa, ta, !0) : new $b(G, qa, ta); wa && K.create(l, G, qa, ta); var a, b; zc && G.getBoundingClientRect && (a = function () { U(G, { left: 0, top: 0 }); b = G.getBoundingClientRect(); U(G, { left: -(b.left - Q(b.left)) + ia, top: -(b.top - Q(b.top)) + ia }) }, a(), ja(ea, "resize", a), ja(l, "destroy", function () { Na(ea, "resize", a) }))
        } function n() {
            function a(c) {
                var d = q.width || Ea.offsetWidth, e = q.height || Ea.offsetHeight, c = c.target; if (d && e && (c === ea || c === V)) {
                    if (d !== na || e !== za) clearTimeout(b), b = setTimeout(function () { zb(d, e, !1) }, 100); na =
d; za = e
                } 
            } var b; ja(ea, "resize", a); ja(l, "destroy", function () { Na(ea, "resize", a) })
        } function u() { l && Z(l, "endResize", null, function () { Ja -= 1 }) } function t() { for (var b = X || q.inverted || q.type === "bar" || q.defaultSeriesType === "bar", c = a.series, d = c && c.length; !b && d--; ) c[d].type === "bar" && (b = !0); l.inverted = X = b } function D() {
            var b = a.labels, c = a.credits, d; j(); Yb = l.legend = new Tb; o(va, function (a) { a.setScale() }); Zb(); o(va, function (a) { a.setTickPositions(!0) }); g(); Zb(); ob(); Ob && o(va, function (a) { a.render() }); if (!l.seriesGroup) l.seriesGroup =
K.g("series-group").attr({ zIndex: 3 }).add(); o(Y, function (a) { a.translate(); a.setTooltipPoints(); a.render() }); b.items && o(b.items, function () { var a = N(b.style, this.style), c = Q(a.left) + S, d = Q(a.top) + L + 12; delete a.left; delete a.top; K.text(this.html, c, d).attr({ zIndex: 2 }).css(a).add() }); if (c.enabled && !l.credits) d = c.href, l.credits = K.text(c.text, 0, 0).on("click", function () { if (d) location.href = d }).attr({ align: c.position.align, zIndex: 8 }).css(c.style).add().align(c.position); Aa(); l.hasRendered = !0; aa && (Ea.appendChild(G),
Vb(aa))
        } function x() {
            if (!Qb && ea == ea.top && V.readyState !== "complete" || wa && !ea.canvg) wa ? Ac.push(x, a.global.canvasToolsURL) : V.attachEvent("onreadystatechange", function () { V.detachEvent("onreadystatechange", x); V.readyState === "complete" && x() }); else {
                m(); Z(l, "init"); if (Highcharts.RangeSelector && a.rangeSelector.enabled) l.rangeSelector = new Highcharts.RangeSelector(l); pb(); xb(); t(); i(); o(a.series || [], function (a) { f(a) }); if (Highcharts.Scroller && (a.navigator.enabled || a.scrollbar.enabled)) l.scroller = new Highcharts.Scroller(l);
                l.render = D; l.tracker = rb = new e(a.tooltip); D(); K.draw(); b && b.apply(l, [l]); o(l.callbacks, function (a) { a.apply(l, [l]) }); Z(l, "load")
            } 
        } var E = a.series; a.series = null; a = M(Ba, a); a.series = E; var q = a.chart, E = q.margin, E = kb(E) ? E : [E, E, E, E], w = p(q.marginTop, E[0]), v = p(q.marginRight, E[1]), Sa = p(q.marginBottom, E[2]), A = p(q.marginLeft, E[3]), C = q.spacingTop, F = q.spacingRight, T = q.spacingBottom, gb = q.spacingLeft, R, fa, da, L, ka, O, S, sa, Ea, aa, G, Oa, na, za, qa, ta, bb, ab, Ha, Pa, Xa, pa, l = this, Bb = (E = q.events) && !!E.click, Ka, Eb, sb, Ya, ub, wb, Ia,
ma, la, rb, Wa, Aa, Yb, Fa, vb, Va, Ob = q.showAxes, Ja = 0, va = [], hb, Y = [], X, K, mb, Cb, nb, ob, Zb, pb, xb, zb, Ab, Ib, Tb = function () {
    function a(b, c) { var d = b.legendItem, e = b.legendLine, g = b.legendSymbol, h = q.color, i = c ? f.itemStyle.color : h, h = c ? b.color : h; d && d.css({ fill: i }); e && e.attr({ stroke: h }); g && g.attr({ stroke: h, fill: h }) } function b(a) {
        var c = a.legendItem, d = a.legendLine, e = a._legendItemPos, f = e[0], e = e[1], g = a.legendSymbol, a = a.checkbox; c && c.attr({ x: v ? f : Fa - f, y: e }); d && d.translate(v ? f : Fa - f, e - 4); g && (c = f + g.xOff, g.attr({ x: v ? c : Fa - c, y: e + g.yOff }));
        if (a) a.x = f, a.y = e
    } function c() { o(j, function (a) { var b = a.checkbox, c = r.alignAttr; b && U(b, { left: c.translateX + a.legendItemWidth + b.x - 40 + ia, top: c.translateY + b.y - 11 + ia }) }) } function d(b) {
        var c, e, j, k, l = b.legendItem; k = b.series || b; var o = k.options, F = o && o.borderWidth || 0; if (!l) {
            k = /^(bar|pie|area|column)$/.test(k.type); b.legendItem = l = K.text(f.labelFormatter.call(b), 0, 0, f.useHTML).css(b.visible ? n : q).on("mouseover", function () { b.setState(Ra); l.css(m) }).on("mouseout", function () { l.css(b.visible ? n : q); b.setState() }).on("click",
function () { var a = function () { b.setVisible() }; b.firePointEvent ? b.firePointEvent("legendItemClick", null, a) : Z(b, "legendItemClick", null, a) }).attr({ align: v ? "left" : "right", zIndex: 2 }).add(r); if (!k && o && o.lineWidth) { var T = { "stroke-width": o.lineWidth, zIndex: 2 }; if (o.dashStyle) T.dashstyle = o.dashStyle; b.legendLine = K.path([ua, (-h - i) * (v ? 1 : -1), 0, ha, -i * (v ? 1 : -1), 0]).attr(T).add(r) } if (k) j = K.rect(c = -h - i, e = -11, h, 12, 2).attr({ zIndex: 3 }).add(r), v || (c += h); else if (o && o.marker && o.marker.enabled) j = o.marker.radius, j = K.symbol(b.symbol,
c = -h / 2 - i - j, e = -4 - j, 2 * j, 2 * j).attr(b.pointAttr[Ga]).attr({ zIndex: 3 }).add(r), v || (c += h / 2); if (j) j.xOff = c + F % 2 / 2, j.yOff = e + F % 2 / 2; b.legendSymbol = j; a(b, b.visible); if (o && o.showCheckbox) b.checkbox = xa("input", { type: "checkbox", checked: b.selected, defaultChecked: b.selected }, f.itemCheckboxStyle, G), ja(b.checkbox, "click", function (a) { Z(b, "checkboxClick", { checked: a.target.checked }, function () { b.select() }) })
        } c = l.getBBox(); e = b.legendItemWidth = f.itemWidth || h + i + c.width + u; w = c.height; if (g && x - t + e > (C || qa - 2 * u - t)) x = t, p += E + w + y; D =
p + y; b._legendItemPos = [x, p]; g ? x += e : p += E + w + y; A = C || $(g ? x - t : e, A)
    } function e() {
        x = t; p = u + E + F - 5; D = A = 0; r || (r = K.g("legend").attr({ zIndex: 10 }).add()); j = []; o(L, function (a) { var b = a.options; b.showInLegend && (j = j.concat(a.legendItems || (b.legendType === "point" ? a.data : a))) }); Kc(j, function (a, b) { return (a.options.legendIndex || 0) - (b.options.legendIndex || 0) }); O && j.reverse(); o(j, d); Fa = C || A; vb = D - F + w; if (Sa || B) {
            Fa += 2 * u; vb += 2 * u; if (T) { if (Fa > 0 && vb > 0) T[T.isNew ? "attr" : "animate"](T.crisp(null, null, null, Fa, vb)), T.isNew = !1 } else T = K.rect(0,
0, Fa, vb, f.borderRadius, Sa || 0).attr({ stroke: f.borderColor, "stroke-width": Sa || 0, fill: B || ga }).add(r).shadow(f.shadow), T.isNew = !0; T[j.length ? "show" : "hide"]()
        } o(j, b); for (var a = ["left", "right", "top", "bottom"], g, h = 4; h--; ) g = a[h], k[g] && k[g] !== "auto" && (f[h < 2 ? "align" : "verticalAlign"] = g, f[h < 2 ? "x" : "y"] = Q(k[g]) * (h % 2 ? -1 : 1)); j.length && r.align(N(f, { width: Fa, height: vb }), !0, R); Ja || c()
    } var f = l.options.legend; if (f.enabled) {
        var g = f.layout === "horizontal", h = f.symbolWidth, i = f.symbolPadding, j, k = f.style, n = f.itemStyle, m = f.itemHoverStyle,
q = M(n, f.itemHiddenStyle), u = f.padding || Q(k.padding), v = !f.rtl, F = 18, t = 4 + u + h + i, x, p, D, w = 0, E = f.itemMarginTop || 0, y = f.itemMarginBottom || 0, T, Sa = f.borderWidth, B = f.backgroundColor, r, A, C = f.width, L = l.series, O = f.reversed; e(); ja(l, "endResize", c); return { colorizeItem: a, destroyItem: function (a) { var b = a.checkbox; o(["legendItem", "legendLine", "legendSymbol"], function (b) { a[b] && a[b].destroy() }); b && Vb(a.checkbox) }, renderLegend: e, destroy: function () { T && (T = T.destroy()); r && (r = r.destroy()) } }
    } 
}; Eb = function (a, b) {
    return a >= 0 && a <=
la && b >= 0 && b <= ma
}; Ib = function () { var a = l.resetZoomButton; Z(l, "selection", { resetSelection: !0 }, Ab); if (a) l.resetZoomButton = a.destroy() }; Ab = function (a) { var b = l.pointCount < 100, c; l.resetZoomEnabled !== !1 && !l.resetZoomButton && k(); !a || a.resetSelection ? o(va, function (a) { a.options.zoomEnabled !== !1 && (a.setExtremes(null, null, !1), c = !0) }) : o(a.xAxis.concat(a.yAxis), function (a) { var b = a.axis; if (l.tracker[b.isXAxis ? "zoomX" : "zoomY"]) b.setExtremes(a.min, a.max, !1), c = !0 }); c && h(!0, b) }; l.pan = function (a) {
    var b = l.xAxis[0], c = l.mouseDownX,
d = b.pointRange / 2, e = b.getExtremes(), f = b.translate(c - a, !0) + d, c = b.translate(c + la - a, !0) - d; (d = l.hoverPoints) && o(d, function (a) { a.setState() }); f > Za(e.dataMin, e.min) && c < $(e.dataMax, e.max) && b.setExtremes(f, c, !0, !1); l.mouseDownX = a; U(G, { cursor: "move" })
}; Zb = function () {
    var b = a.legend, c = p(b.margin, 10), d = b.x, e = b.y, f = b.align, g = b.verticalAlign, h; pb(); if ((l.title || l.subtitle) && !y(w)) (h = $(l.title && !fa.floating && !fa.verticalAlign && fa.y || 0, l.subtitle && !da.floating && !da.verticalAlign && da.y || 0)) && (L = $(L, h + p(fa.margin, 15) +
C)); b.enabled && !b.floating && (f === "right" ? y(v) || (ka = $(ka, Fa - d + c + F)) : f === "left" ? y(A) || (S = $(S, Fa + d + c + gb)) : g === "top" ? y(w) || (L = $(L, vb + e + c + C)) : g === "bottom" && (y(Sa) || (O = $(O, vb - e + c + T)))); l.extraBottomMargin && (O += l.extraBottomMargin); l.extraTopMargin && (L += l.extraTopMargin); Ob && o(va, function (a) { a.getOffset() }); y(A) || (S += sa[3]); y(w) || (L += sa[0]); y(Sa) || (O += sa[2]); y(v) || (ka += sa[1]); xb()
}; zb = function (a, b, c) {
    var d = l.title, e = l.subtitle; Ja += 1; Lb(c, l); ab = ta; bb = qa; if (y(a)) l.chartWidth = qa = B(a); if (y(b)) l.chartHeight =
ta = B(b); U(G, { width: qa + ia, height: ta + ia }); K.setSize(qa, ta, c); la = qa - S - ka; ma = ta - L - O; hb = null; o(va, function (a) { a.isDirty = !0; a.setScale() }); o(Y, function (a) { a.isDirty = !0 }); l.isDirtyLegend = !0; l.isDirtyBox = !0; Zb(); d && d.align(null, null, R); e && e.align(null, null, R); h(c); ab = null; Z(l, "resize"); Wb === !1 ? u() : setTimeout(u, Wb && Wb.duration || 500)
}; xb = function () {
    l.plotLeft = S = B(S); l.plotTop = L = B(L); l.plotWidth = la = B(qa - S - ka); l.plotHeight = ma = B(ta - L - O); l.plotSizeX = X ? ma : la; l.plotSizeY = X ? la : ma; R = { x: gb, y: C, width: qa - gb - F, height: ta -
C - T
    }; o(va, function (a) { a.setAxisSize(); a.setAxisTranslation() })
}; pb = function () { L = p(w, C); ka = p(v, F); O = p(Sa, T); S = p(A, gb); sa = [0, 0, 0, 0] }; ob = function () {
    var a = q.borderWidth || 0, b = q.backgroundColor, c = q.plotBackgroundColor, d = q.plotBackgroundImage, e, f = { x: S, y: L, width: la, height: ma }; e = a + (q.shadow ? 8 : 0); if (a || b) Ha ? Ha.animate(Ha.crisp(null, null, null, qa - e, ta - e)) : Ha = K.rect(e / 2, e / 2, qa - e, ta - e, q.borderRadius, a).attr({ stroke: q.borderColor, "stroke-width": a, fill: b || ga }).add().shadow(q.shadow); c && (Pa ? Pa.animate(f) : Pa = K.rect(S,
L, la, ma, 0).attr({ fill: c }).add().shadow(q.plotShadow)); d && (Xa ? Xa.animate(f) : Xa = K.image(d, S, L, la, ma).add()); q.plotBorderWidth && (pa ? pa.animate(pa.crisp(null, S, L, la, ma)) : pa = K.rect(S, L, la, ma, 0, q.plotBorderWidth).attr({ stroke: q.plotBorderColor, "stroke-width": q.plotBorderWidth, zIndex: 4 }).add()); l.isDirtyBox = !1
}; q.reflow !== !1 && ja(l, "load", n); if (E) for (Ka in E) ja(l, Ka, E[Ka]); l.options = a; l.series = Y; l.xAxis = []; l.yAxis = []; l.addSeries = function (a, b, c) {
    var d; a && (Lb(c, l), b = p(b, !0), Z(l, "addSeries", { options: a }, function () {
        d =
f(a); d.isDirty = !0; l.isDirtyLegend = !0; b && l.redraw()
    })); return d
}; l.animation = wa ? !1 : p(q.animation, !0); l.Axis = c; l.destroy = function () {
    var b, c = G && G.parentNode; if (l !== null) {
        Z(l, "destroy"); Na(l); for (b = va.length; b--; ) va[b] = va[b].destroy(); for (b = Y.length; b--; ) Y[b] = Y[b].destroy(); o("title,subtitle,seriesGroup,clipRect,credits,tracker,scroller,rangeSelector".split(","), function (a) { var b = l[a]; b && (l[a] = b.destroy()) }); o([Ha, pa, Pa, Yb, sb, K, rb], function (a) { a && a.destroy && a.destroy() }); Ha = pa = Pa = Yb = sb = K = rb = null; if (G) G.innerHTML =
"", Na(G), c && Vb(G), G = null; clearInterval(Cb); for (b in l) delete l[b]; a = l = null
    } 
}; l.get = function (a) { var b, c, d; for (b = 0; b < va.length; b++) if (va[b].options.Id === a) return va[b]; for (b = 0; b < Y.length; b++) if (Y[b].options.Id === a) return Y[b]; for (b = 0; b < Y.length; b++) { d = Y[b].points || []; for (c = 0; c < d.length; c++) if (d[c].Id === a) return d[c] } return null }; l.getSelectedPoints = function () { var a = []; o(Y, function (b) { a = a.concat(mc(b.points, function (a) { return a.selected })) }); return a }; l.getSelectedSeries = function () { return mc(Y, function (a) { return a.selected }) };
        l.hideLoading = function () { ub && ec(ub, { opacity: 0 }, { duration: a.loading.hideDuration || 100, complete: function () { U(ub, { display: ga }) } }); Ia = !1 }; l.initSeries = f; l.isInsidePlot = Eb; l.redraw = h; l.setSize = zb; l.setTitle = j; l.showLoading = function (b) {
            var c = a.loading; ub || (ub = xa(db, { className: Da + "loading" }, N(c.style, { left: S + ia, top: L + ia, width: la + ia, height: ma + ia, zIndex: 10, display: ga }), G), wb = xa("span", null, c.labelStyle, ub)); wb.innerHTML = b || a.lang.loading; Ia || (U(ub, { opacity: 0, display: "" }), ec(ub, { opacity: c.style.opacity }, { duration: c.showDuration ||
0
            }), Ia = !0)
        }; l.pointCount = 0; l.counters = new sc; x()
    } var ba, V = document, ea = window, oa = Math, B = oa.round, Ta = oa.floor, ac = oa.ceil, $ = oa.max, Za = oa.min, ya = oa.abs, fa = oa.cos, sa = oa.sin, ab = oa.PI, Bc = ab * 2 / 360, Ha = navigator.userAgent, Sb = /msie/i.test(Ha) && !ea.opera, wb = V.documentMode === 8, Cc = /AppleWebKit/.test(Ha), zc = /Firefox/.test(Ha), Qb = !!V.createElementNS && !!V.createElementNS("http://www.w3.org/2000/svg", "svg").createSVGRect, Oc = zc && parseInt(Ha.split("Firefox/")[1], 10) < 4, wa = !Qb && !Sb && !!V.createElement("canvas").getContext,
$b, ra = V.documentElement.ontouchstart !== ba, Dc = {}, lc = 0, pb, Ba, dc, Wb, Ia, C, db = "div", tb = "absolute", yc = "relative", $a = "hidden", Da = "highcharts-", eb = "visible", ia = "px", ga = "none", ua = "M", ha = "L", Ec = "rgba(192,192,192," + (Qb ? 1.0E-6 : 0.0020) + ")", Ga = "", Ra = "hover", Ab = "millisecond", mb = "second", Bb = "minute", Pa = "hour", na = "day", Ka = "week", Xa = "month", za = "year", ob, Ib, Tb, ic, Ya, Cb, nb, oc, pc, hc, qc, rc, A = ea.HighchartsAdapter, aa = A || {}, Fc = aa.getScript, o = aa.each, mc = aa.grep, xc = aa.offset, Xb = aa.map, M = aa.merge, ja = aa.addEvent, Na = aa.removeEvent,
Z = aa.fireEvent, ec = aa.animate, Pb = aa.stop, Ca = {}; ea.Highcharts = {}; dc = function (a, b, c) {
    function d(a, b) { a = a.toString().replace(/^([0-9])$/, "0$1"); b === 3 && (a = a.toString().replace(/^([0-9]{2})$/, "0$1")); return a } if (!y(b) || isNaN(b)) return "Invalid date"; var a = p(a, "%Y-%m-%d %H:%M:%S"), e = new Date(b), f, g = e[Tb](), h = e[ic](), i = e[Ya](), k = e[Cb](), j = e[nb](), m = Ba.lang, n = m.weekdays, b = { a: n[h].substr(0, 3), A: n[h], d: d(i), e: i, b: m.shortMonths[k], B: m.months[k], m: d(k + 1), y: j.toString().substr(2, 2), Y: j, H: d(g), I: d(g % 12 || 12), l: g %
12 || 12, M: d(e[Ib]()), p: g < 12 ? "AM" : "PM", P: g < 12 ? "am" : "pm", S: d(e.getSeconds()), L: d(b % 1E3, 3)
    }; for (f in b) a = a.replace("%" + f, b[f]); return c ? a.substr(0, 1).toUpperCase() + a.substr(1) : a
}; sc.prototype = { wrapColor: function (a) { if (this.color >= a) this.color = 0 }, wrapSymbol: function (a) { if (this.symbol >= a) this.symbol = 0 } }; C = Oa(Ab, 1, mb, 1E3, Bb, 6E4, Pa, 36E5, na, 864E5, Ka, 6048E5, Xa, 2592E6, za, 31556952E3); Ia = { init: function (a, b, c) {
    var b = b || "", d = a.shift, e = b.indexOf("C") > -1, f = e ? 7 : 3, g, b = b.split(" "), c = [].concat(c), h, i, k = function (a) {
        for (g =
a.length; g--; ) a[g] === ua && a.splice(g + 1, 0, a[g + 1], a[g + 2], a[g + 1], a[g + 2])
    }; e && (k(b), k(c)); a.isArea && (h = b.splice(b.length - 6, 6), i = c.splice(c.length - 6, 6)); d === 1 && (c = [].concat(c).splice(0, f).concat(c)); a.shift = 0; if (b.length) for (a = c.length; b.length < a; ) d = [].concat(b).splice(b.length - f, f), e && (d[f - 6] = d[f - 2], d[f - 5] = d[f - 1]), b = b.concat(d); h && (b = b.concat(h), c = c.concat(i)); return [b, c]
}, step: function (a, b, c, d) {
    var e = [], f = a.length; if (c === 1) e = d; else if (f === b.length && c < 1) for (; f--; ) d = parseFloat(a[f]), e[f] = isNaN(d) ? a[f] :
c * parseFloat(b[f] - d) + d; else e = b; return e
} 
}; A && A.init && A.init(Ia); if (!A && ea.jQuery) {
        var da = jQuery, Fc = da.getScript, o = function (a, b) { for (var c = 0, d = a.length; c < d; c++) if (b.call(a[c], a[c], c, a) === !1) return c }, mc = da.grep, Xb = function (a, b) { for (var c = [], d = 0, e = a.length; d < e; d++) c[d] = b.call(a[d], a[d], d, a); return c }, M = function () { var a = arguments; return da.extend(!0, null, a[0], a[1], a[2], a[3]) }, xc = function (a) { return da(a).offset() }, ja = function (a, b, c) { da(a).bind(b, c) }, Na = function (a, b, c) {
            var d = V.removeEventListener ? "removeEventListener" :
"detachEvent"; V[d] && !a[d] && (a[d] = function () { }); da(a).unbind(b, c)
        }, Z = function (a, b, c, d) { var e = da.Event(b), f = "detached" + b, g; N(e, c); a[b] && (a[f] = a[b], a[b] = null); o(["preventDefault", "stopPropagation"], function (a) { var b = e[a]; e[a] = function () { try { b.call(e) } catch (c) { a === "preventDefault" && (g = !0) } } }); da(a).trigger(e); a[f] && (a[b] = a[f], a[f] = null); d && !e.isDefaultPrevented() && !g && d(e) }, ec = function (a, b, c) { var d = da(a); if (b.d) a.toD = b.d, b.d = 1; d.stop(); d.animate(b, c) }, Pb = function (a) { da(a).stop() }; da.extend(da.easing,
{ easeOutQuad: function (a, b, c, d, e) { return -d * (b /= e) * (b - 2) + c } }); var Gc = jQuery.fx, Hc = Gc.step; o(["cur", "_default", "width", "height"], function (a, b) { var c = b ? Hc : Gc.prototype, d = c[a], e; d && (c[a] = function (a) { a = b ? a : this; e = a.elem; return e.attr ? e.attr(a.prop, a.now) : d.apply(this, arguments) }) }); Hc.d = function (a) { var b = a.elem; if (!a.started) { var c = Ia.init(b, b.d, b.toD); a.start = c[0]; a.end = c[1]; a.started = !0 } b.attr("d", Ia.step(a.start, a.end, a.pos, b.toD)) } 
    } A = { enabled: !0, align: "center", x: 0, y: 15, style: { color: "#666", fontSize: "11px",
        lineHeight: "14px"
    }
    }; Ba = { colors: "#4572A7,#AA4643,#89A54E,#80699B,#3D96AE,#DB843D,#92A8CD,#A47D7C,#B5CA92".split(","), symbols: ["circle", "diamond", "square", "triangle", "triangle-down"], lang: { loading: "Loading...", months: "January,February,March,April,May,June,July,August,September,October,November,December".split(","), shortMonths: "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(","), weekdays: "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday".split(","), decimalPoint: ".", resetZoom: "Reset zoom",
        resetZoomTitle: "Reset zoom level 1:1", thousandsSep: ","
    }, global: { useUTC: !0, canvasToolsURL: "http://code.highcharts.com/2.2.0/modules/canvas-tools.js" }, chart: { borderColor: "#4572A7", borderRadius: 5, defaultSeriesType: "line", ignoreHiddenSeries: !0, spacingTop: 10, spacingRight: 10, spacingBottom: 15, spacingLeft: 10, style: { fontFamily: '"Lucida Grande", "Lucida Sans Unicode", Verdana, Arial, Helvetica, sans-serif', fontSize: "12px" }, backgroundColor: "#FFFFFF", plotBorderColor: "#C0C0C0", resetZoomButton: { theme: { zIndex: 20 },
        position: { align: "right", x: -10, y: 10}
    }
    }, title: { text: "Chart title", align: "center", y: 15, style: { color: "#3E576F", fontSize: "16px"} }, subtitle: { text: "", align: "center", y: 30, style: { color: "#6D869F"} }, plotOptions: { line: { allowPointSelect: !1, showCheckbox: !1, animation: { duration: 1E3 }, events: {}, lineWidth: 2, shadow: !0, marker: { enabled: !0, lineWidth: 0, radius: 4, lineColor: "#FFFFFF", states: { hover: {}, select: { fillColor: "#FFFFFF", lineColor: "#000000", lineWidth: 2}} }, point: { events: {} }, dataLabels: M(A, { enabled: !1, y: -6, formatter: function () { return this.y } }),
        cropThreshold: 300, pointRange: 0, showInLegend: !0, states: { hover: { marker: {} }, select: { marker: {}} }, stickyTracking: !0
    }
    }, labels: { style: { position: tb, color: "#3E576F"} }, legend: { enabled: !0, align: "center", layout: "horizontal", labelFormatter: function () { return this.name }, borderWidth: 1, borderColor: "#909090", borderRadius: 5, shadow: !1, style: { padding: "5px" }, itemStyle: { cursor: "pointer", color: "#3E576F" }, itemHoverStyle: { color: "#000000" }, itemHiddenStyle: { color: "#C0C0C0" }, itemCheckboxStyle: { position: tb, width: "13px", height: "13px" },
        symbolWidth: 16, symbolPadding: 5, verticalAlign: "bottom", x: 0, y: 0
    }, loading: { labelStyle: { fontWeight: "bold", position: yc, top: "1em" }, style: { position: tb, backgroundColor: "white", opacity: 0.5, textAlign: "center"} }, tooltip: { enabled: !0, backgroundColor: "rgba(255, 255, 255, .85)", borderWidth: 2, borderRadius: 5, headerFormat: '<span style="font-size: 10px">{point.key}</span><br/>', pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b><br/>', shadow: !0, shared: wa, snap: ra ? 25 : 10, style: { color: "#333333",
        fontSize: "12px", padding: "5px", whiteSpace: "nowrap"
    }
    }, credits: { enabled: !0, text: "", href: "#", position: { align: "right", x: -10, verticalAlign: "bottom", y: -5 }, style: { cursor: "pointer", color: "#909090", fontSize: "10px"}}
    }; var cc = { dateTimeLabelFormats: Oa(Ab, "%H:%M:%S.%L", mb, "%H:%M:%S", Bb, "%H:%M", Pa, "%H:%M", na, "%e. %b", Ka, "%e. %b", Xa, "%b '%y", za, "%Y"), endOnTick: !1, gridLineColor: "#C0C0C0", labels: A, lineColor: "#C0D0E0", lineWidth: 1, max: null, min: null, minPadding: 0.01, maxPadding: 0.01,
        minorGridLineColor: "#E0E0E0", minorGridLineWidth: 1, minorTickColor: "#A0A0A0", minorTickLength: 2, minorTickPosition: "outside", startOfWeek: 1, startOnTick: !1, tickColor: "#C0D0E0", tickLength: 5, tickmarkPlacement: "between", tickPixelInterval: 100, tickPosition: "outside", tickWidth: 1, title: { align: "middle", style: { color: "#6D869F", fontWeight: "bold"} }, type: "linear"
    }, kc = M(cc, { endOnTick: !0, gridLineWidth: 1, tickPixelInterval: 72, showLastLabel: !0, labels: { align: "right", x: -8, y: 3 }, lineWidth: 0, maxPadding: 0.05, minPadding: 0.05, startOnTick: !0,
        tickWidth: 0, title: { rotation: 270, text: "Y-values" }, stackLabels: { enabled: !1, formatter: function () { return this.total }, style: A.style}
    }), Nc = { labels: { align: "right", x: -8, y: null }, title: { rotation: 270} }, Mc = { labels: { align: "left", x: 8, y: null }, title: { rotation: 90} }, wc = { labels: { align: "center", x: 0, y: 14 }, title: { rotation: 0} }, Lc = M(wc, { labels: { y: -5} }), ka = Ba.plotOptions, A = ka.line; ka.spline = M(A); ka.scatter = M(A, { lineWidth: 0, states: { hover: { lineWidth: 0} }, tooltip: { headerFormat: '<span style="font-size: 10px; color:{series.color}">{series.name}</span><br/>',
        pointFormat: "x: <b>{point.x}</b><br/>y: <b>{point.y}</b><br/>"
    }
    }); ka.area = M(A, { threshold: 0 }); ka.areaspline = M(ka.area); ka.column = M(A, { borderColor: "#FFFFFF", borderWidth: 1, borderRadius: 0, groupPadding: 0.2, marker: null, pointPadding: 0.1, minPointLength: 0, cropThreshold: 50, pointRange: null, states: { hover: { brightness: 0.1, shadow: !1 }, select: { color: "#C0C0C0", borderColor: "#000000", shadow: !1} }, dataLabels: { y: null, verticalAlign: null }, threshold: 0 }); ka.bar = M(ka.column, { dataLabels: { align: "left", x: 5, y: 0} }); ka.pie = M(A, { borderColor: "#FFFFFF",
        borderWidth: 1, center: ["50%", "50%"], colorByPoint: !0, dataLabels: { distance: 30, enabled: !0, formatter: function () { return this.point.name }, y: 5 }, legendType: "point", marker: null, size: "75%", showInLegend: !1, slicedOffset: 10, states: { hover: { brightness: 0.1, shadow: !1}}
    }); uc(); var bb = function (a) {
        var b = [], c; (function (a) {
            (c = /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]?(?:\.[0-9]+)?)\s*\)/.exec(a)) ? b = [Q(c[1]), Q(c[2]), Q(c[3]), parseFloat(c[4], 10)] : (c = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(a)) &&
(b = [Q(c[1], 16), Q(c[2], 16), Q(c[3], 16), 1])
        })(a); return { get: function (c) { return b && !isNaN(b[0]) ? c === "rgb" ? "rgb(" + b[0] + "," + b[1] + "," + b[2] + ")" : c === "a" ? b[3] : "rgba(" + b.join(",") + ")" : a }, brighten: function (a) { if (Ja(a) && a !== 0) { var c; for (c = 0; c < 3; c++) b[c] += Q(a * 255), b[c] < 0 && (b[c] = 0), b[c] > 255 && (b[c] = 255) } return this }, setOpacity: function (a) { b[3] = a; return this } }
    }; pa.prototype = { init: function (a, b) { this.element = b === "span" ? xa(b) : V.createElementNS("http://www.w3.org/2000/svg", b); this.renderer = a; this.attrSetters = {} }, animate: function (a,
b, c) { b = p(b, Wb, !0); Pb(this); if (b) { b = M(b); if (c) b.complete = c; ec(this, a, b) } else this.attr(a), c && c() }, attr: function (a, b) {
    var c, d, e, f, g = this.element, h = g.nodeName, i = this.renderer, k, j = this.attrSetters, m = this.shadows, n, o = this; yb(a) && y(b) && (c = a, a = {}, a[c] = b); if (yb(a)) c = a, h === "circle" ? c = { x: "cx", y: "cy"}[c] || c : c === "strokeWidth" && (c = "stroke-width"), o = r(g, c) || this[c] || 0, c !== "d" && c !== "visibility" && (o = parseFloat(o)); else for (c in a) if (k = !1, d = a[c], e = j[c] && j[c](d, c), e !== !1) {
        e !== ba && (d = e); if (c === "d") d && d.join && (d = d.join(" ")),
/(NaN| {2}|^$)/.test(d) && (d = "M 0 0"), this.d = d; else if (c === "x" && h === "text") { for (e = 0; e < g.childNodes.length; e++) f = g.childNodes[e], r(f, "x") === r(g, "x") && r(f, "x", d); this.rotation && r(g, "transform", "rotate(" + this.rotation + " " + d + " " + Q(a.y || r(g, "y")) + ")") } else if (c === "fill") d = i.color(d, g, c); else if (h === "circle" && (c === "x" || c === "y")) c = { x: "cx", y: "cy"}[c] || c; else if (h === "rect" && c === "r") r(g, { rx: d, ry: d }), k = !0; else if (c === "translateX" || c === "translateY" || c === "rotation" || c === "verticalAlign") this[c] = d, this.updateTransform(),
k = !0; else if (c === "stroke") d = i.color(d, g, c); else if (c === "dashstyle") if (c = "stroke-dasharray", d = d && d.toLowerCase(), d === "solid") d = ga; else { if (d) { d = d.replace("shortdashdotdot", "3,1,1,1,1,1,").replace("shortdashdot", "3,1,1,1").replace("shortdot", "1,1,").replace("shortdash", "3,1,").replace("longdash", "8,3,").replace(/dot/g, "1,3,").replace("dash", "4,3,").replace(/,$/, "").split(","); for (e = d.length; e--; ) d[e] = Q(d[e]) * a["stroke-width"]; d = d.join(",") } } else c === "isTracker" ? this[c] = d : c === "width" ? d = Q(d) : c === "align" ?
(c = "text-anchor", d = { left: "start", center: "middle", right: "end"}[d]) : c === "title" && (e = V.createElementNS("http://www.w3.org/2000/svg", "title"), e.appendChild(V.createTextNode(d)), g.appendChild(e)); c === "strokeWidth" && (c = "stroke-width"); Cc && c === "stroke-width" && d === 0 && (d = 1.0E-6); this.symbolName && /^(x|y|r|start|end|innerR|anchorX|anchorY)/.test(c) && (n || (this.symbolAttr(a), n = !0), k = !0); if (m && /^(width|height|visibility|x|y|d|transform)$/.test(c)) for (e = m.length; e--; ) r(m[e], c, d); if ((c === "width" || c === "height") &&
h === "rect" && d < 0) d = 0; c === "text" ? (this.textStr = d, this.added && i.buildText(this)) : k || r(g, c, d)
    } if (Cc && /Chrome\/(18|19)/.test(Ha) && h === "text" && (a.x !== ba || a.y !== ba)) c = g.parentNode, d = g.nextSibling, c && (c.removeChild(g), d ? c.insertBefore(g, d) : c.appendChild(g)); return o
}, symbolAttr: function (a) { var b = this; o("x,y,r,start,end,width,height,innerR,anchorX,anchorY".split(","), function (c) { b[c] = p(a[c], b[c]) }); b.attr({ d: b.renderer.symbols[b.symbolName](b.x, b.y, b.width, b.height, b) }) }, clip: function (a) {
    return this.attr("clip-path",
"url(" + this.renderer.url + "#" + a.Id + ")")
}, crisp: function (a, b, c, d, e) { var f, g = {}, h = {}, i, a = a || this.strokeWidth || this.attr && this.attr("stroke-width") || 0; i = B(a) % 2 / 2; h.x = Ta(b || this.x || 0) + i; h.y = Ta(c || this.y || 0) + i; h.width = Ta((d || this.width || 0) - 2 * i); h.height = Ta((e || this.height || 0) - 2 * i); h.strokeWidth = a; for (f in h) this[f] !== h[f] && (this[f] = g[f] = h[f]); return g }, css: function (a) {
    var b = this.element, b = a && a.width && b.nodeName === "text", c, d = "", e = function (a, b) { return "-" + b.toLowerCase() }; if (a && a.color) a.fill = a.color; this.styles =
a = N(this.styles, a); if (Sb && !Qb) b && delete a.width, U(this.element, a); else { for (c in a) d += c.replace(/([A-Z])/g, e) + ":" + a[c] + ";"; this.attr({ style: d }) } b && this.added && this.renderer.buildText(this); return this
}, on: function (a, b) { var c = b; ra && a === "click" && (a = "touchstart", c = function (a) { a.preventDefault(); b() }); this.element["on" + a] = c; return this }, translate: function (a, b) { return this.attr({ translateX: a, translateY: b }) }, invert: function () { this.inverted = !0; this.updateTransform(); return this }, htmlCss: function (a) {
    var b =
this.element; if (b = a && b.tagName === "SPAN" && a.width) delete a.width, this.textWidth = b, this.updateTransform(); this.styles = N(this.styles, a); U(this.element, a); return this
}, htmlGetBBox: function (a) { var b = this.element, c = this.bBox; if (!c || a) { if (b.nodeName === "text") b.style.position = tb; c = this.bBox = { x: b.offsetLeft, y: b.offsetTop, width: b.offsetWidth, height: b.offsetHeight} } return c }, htmlUpdateTransform: function () {
    if (this.added) {
        var a = this, b = a.element, c = a.translateX || 0, d = a.translateY || 0, e = a.x || 0, f = a.y || 0, g = a.textAlign ||
"left", h = { left: 0, center: 0.5, right: 1}[g], i = g && g !== "left", k = a.shadows; if (c || d) U(b, { marginLeft: c, marginTop: d }), k && o(k, function (a) { U(a, { marginLeft: c + 1, marginTop: d + 1 }) }); a.inverted && o(b.childNodes, function (c) { a.renderer.invertChild(c, b) }); if (b.tagName === "SPAN") {
            var j, m, k = a.rotation, n; j = 0; var u = 1, t = 0, D; n = Q(a.textWidth); var x = a.xCorr || 0, E = a.yCorr || 0, q = [k, g, b.innerHTML, a.textWidth].join(","); if (q !== a.cTT) y(k) && (j = k * Bc, u = fa(j), t = sa(j), U(b, { filter: k ? ["progid:DXImageTransform.Microsoft.Matrix(M11=", u, ", M12=",
-t, ", M21=", t, ", M22=", u, ", sizingMethod='auto expand')"].join("") : ga
            })), j = p(a.elemWidth, b.offsetWidth), m = p(a.elemHeight, b.offsetHeight), j > n && (U(b, { width: n + ia, display: "block", whiteSpace: "normal" }), j = n), n = B((Q(b.style.fontSize) || 12) * 1.2), x = u < 0 && -j, E = t < 0 && -m, D = u * t < 0, x += t * n * (D ? 1 - h : h), E -= u * n * (k ? D ? h : 1 - h : 1), i && (x -= j * h * (u < 0 ? -1 : 1), k && (E -= m * h * (t < 0 ? -1 : 1)), U(b, { textAlign: g })), a.xCorr = x, a.yCorr = E; U(b, { left: e + x + ia, top: f + E + ia }); a.cTT = q
        } 
    } else this.alignOnAdd = !0
}, updateTransform: function () {
    var a = this.translateX ||
0, b = this.translateY || 0, c = this.inverted, d = this.rotation, e = []; c && (a += this.attr("width"), b += this.attr("height")); (a || b) && e.push("translate(" + a + "," + b + ")"); c ? e.push("rotate(90) scale(-1,1)") : d && e.push("rotate(" + d + " " + this.x + " " + this.y + ")"); e.length && r(this.element, "transform", e.join(" "))
}, toFront: function () { var a = this.element; a.parentNode.appendChild(a); return this }, align: function (a, b, c) {
    a ? (this.alignOptions = a, this.alignByTranslate = b, c || this.renderer.alignedObjects.push(this)) : (a = this.alignOptions, b =
this.alignByTranslate); var c = p(c, this.renderer), d = a.align, e = a.verticalAlign, f = (c.x || 0) + (a.x || 0), g = (c.y || 0) + (a.y || 0), h = {}; /^(right|center)$/.test(d) && (f += (c.width - (a.width || 0)) / { right: 1, center: 2}[d]); h[b ? "translateX" : "x"] = B(f); /^(bottom|middle)$/.test(e) && (g += (c.height - (a.height || 0)) / ({ bottom: 1, middle: 2}[e] || 1)); h[b ? "translateY" : "y"] = B(g); this[this.placed ? "animate" : "attr"](h); this.placed = !0; this.alignAttr = h; return this
}, getBBox: function (a) {
    var b, c, d = this.rotation; c = this.element; var e = d * Bc; if (c.namespaceURI ===
"http://www.w3.org/2000/svg") { try { b = c.getBBox ? N({}, c.getBBox()) : { width: c.offsetWidth, height: c.offsetHeight} } catch (f) { } if (!b || b.width < 0) b = { width: 0, height: 0 }; a = b.width; c = b.height; if (d) b.width = ya(c * sa(e)) + ya(a * fa(e)), b.height = ya(c * fa(e)) + ya(a * sa(e)) } else b = this.htmlGetBBox(a); return b
}, show: function () { return this.attr({ visibility: eb }) }, hide: function () { return this.attr({ visibility: $a }) }, add: function (a) {
    var b = this.renderer, c = a || b, d = c.element || b.box, e = d.childNodes, f = this.element, g = r(f, "zIndex"), h; this.parentInverted =
a && a.inverted; this.textStr !== void 0 && b.buildText(this); if (g) c.handleZ = !0, g = Q(g); if (c.handleZ) for (c = 0; c < e.length; c++) if (a = e[c], b = r(a, "zIndex"), a !== f && (Q(b) > g || !y(g) && y(b))) { d.insertBefore(f, a); h = !0; break } h || d.appendChild(f); this.added = !0; Z(this, "add"); return this
}, safeRemoveChild: function (a) { var b = a.parentNode; b && b.removeChild(a) }, destroy: function () {
    var a = this, b = a.element || {}, c = a.shadows, d = a.box, e, f; b.onclick = b.onmouseout = b.onmouseover = b.onmousemove = null; Pb(a); if (a.clipPath) a.clipPath = a.clipPath.destroy();
    if (a.stops) { for (f = 0; f < a.stops.length; f++) a.stops[f] = a.stops[f].destroy(); a.stops = null } a.safeRemoveChild(b); c && o(c, function (b) { a.safeRemoveChild(b) }); d && d.destroy(); Gb(a.renderer.alignedObjects, a); for (e in a) delete a[e]; return null
}, empty: function () { for (var a = this.element, b = a.childNodes, c = b.length; c--; ) a.removeChild(b[c]) }, shadow: function (a, b) {
    var c = [], d, e, f = this.element, g = this.parentInverted ? "(-1,-1)" : "(1,1)"; if (a) {
        for (d = 1; d <= 3; d++) e = f.cloneNode(0), r(e, { isShadow: "true", stroke: "rgb(0, 0, 0)", "stroke-opacity": 0.05 *
d, "stroke-width": 7 - 2 * d, transform: "translate" + g, fill: ga
        }), b ? b.element.appendChild(e) : f.parentNode.insertBefore(e, f), c.push(e); this.shadows = c
    } return this
} 
    }; var Fb = function () { this.init.apply(this, arguments) }; Fb.prototype = { Element: pa, init: function (a, b, c, d) {
        var e = location, f; f = this.createElement("svg").attr({ xmlns: "http://www.w3.org/2000/svg", version: "1.1" }); a.appendChild(f.element); this.isSVG = !0; this.box = f.element; this.boxWrapper = f; this.alignedObjects = []; this.url = Sb ? "" : e.href.replace(/#.*?$/, "").replace(/\(/g,
"\\(").replace(/\)/g, "\\)"); this.defs = this.createElement("defs").add(); this.forExport = d; this.gradients = {}; this.setSize(b, c, !1)
    }, destroy: function () { var a = this.defs; this.box = null; this.boxWrapper = this.boxWrapper.destroy(); Kb(this.gradients || {}); this.gradients = null; if (a) this.defs = a.destroy(); return this.alignedObjects = null }, createElement: function (a) { var b = new this.Element; b.init(this, a); return b }, draw: function () { }, buildText: function (a) {
        for (var b = a.element, c = p(a.textStr, "").toString().replace(/<(b|strong)>/g,
'<span style="font-weight:bold">').replace(/<(i|em)>/g, '<span style="font-style:italic">').replace(/<a/g, "<span").replace(/<\/(b|strong|i|em|a)>/g, "</span>").split(/<br.*?>/g), d = b.childNodes, e = /style="([^"]+)"/, f = /href="([^"]+)"/, g = r(b, "x"), h = a.styles, i = h && Q(h.width), k = h && h.lineHeight, j, h = d.length; h--; ) b.removeChild(d[h]); i && !a.added && this.box.appendChild(b); c[c.length - 1] === "" && c.pop(); o(c, function (c, d) {
    var h, t = 0, p, c = c.replace(/<span/g, "|||<span").replace(/<\/span>/g, "</span>|||"); h = c.split("|||");
    o(h, function (c) {
        if (c !== "" || h.length === 1) {
            var m = {}, o = V.createElementNS("http://www.w3.org/2000/svg", "tspan"); e.test(c) && r(o, "style", c.match(e)[1].replace(/(;| |^)color([ :])/, "$1fill$2")); f.test(c) && (r(o, "onclick", 'location.href="' + c.match(f)[1] + '"'), U(o, { cursor: "pointer" })); c = (c.replace(/<(.|\n)*?>/g, "") || " ").replace(/&lt;/g, "<").replace(/&gt;/g, ">"); o.appendChild(V.createTextNode(c)); t ? m.dx = 3 : m.x = g; if (!t) {
                if (d) {
                    !Qb && a.renderer.forExport && U(o, { display: "block" }); p = ea.getComputedStyle && Q(ea.getComputedStyle(j,
null).getPropertyValue("line-height")); if (!p || isNaN(p)) p = k || j.offsetHeight || 18; r(o, "dy", p)
                } j = o
            } r(o, m); b.appendChild(o); t++; if (i) for (var c = c.replace(/-/g, "- ").split(" "), w, v = []; c.length || v.length; ) w = a.getBBox().width, m = w > i, !m || c.length === 1 ? (c = v, v = [], c.length && (o = V.createElementNS("http://www.w3.org/2000/svg", "tspan"), r(o, { dy: k || 16, x: g }), b.appendChild(o), w > i && (i = w))) : (o.removeChild(o.firstChild), v.unshift(c.pop())), c.length && o.appendChild(V.createTextNode(c.join(" ").replace(/- /g, "-")))
        } 
    })
})
    }, button: function (a,
b, c, d, e, f, g) {
        var h = this.label(a, b, c), i = 0, k, j, m, n, o, a = { x1: 0, y1: 0, x2: 0, y2: 1 }, e = M(Oa("stroke-width", 1, "stroke", "#999", "fill", Oa("linearGradient", a, "stops", [[0, "#FFF"], [1, "#DDD"]]), "r", 3, "padding", 3, "style", Oa("color", "black")), e); m = e.style; delete e.style; f = M(e, Oa("stroke", "#68A", "fill", Oa("linearGradient", a, "stops", [[0, "#FFF"], [1, "#ACF"]])), f); n = f.style; delete f.style; g = M(e, Oa("stroke", "#68A", "fill", Oa("linearGradient", a, "stops", [[0, "#9BD"], [1, "#CDF"]])), g); o = g.style; delete g.style; ja(h.element, "mouseenter",
function () { h.attr(f).css(n) }); ja(h.element, "mouseleave", function () { k = [e, f, g][i]; j = [m, n, o][i]; h.attr(k).css(j) }); h.setState = function (a) { (i = a) ? a === 2 && h.attr(g).css(o) : h.attr(e).css(m) }; return h.on("click", function () { d.call(h) }).attr(e).css(N({ cursor: "default" }, m))
    }, crispLine: function (a, b) { a[1] === a[4] && (a[1] = a[4] = B(a[1]) + b % 2 / 2); a[2] === a[5] && (a[2] = a[5] = B(a[2]) + b % 2 / 2); return a }, path: function (a) { return this.createElement("path").attr({ d: a, fill: ga }) }, circle: function (a, b, c) { a = kb(a) ? a : { x: a, y: b, r: c }; return this.createElement("circle").attr(a) },
        arc: function (a, b, c, d, e, f) { if (kb(a)) b = a.y, c = a.r, d = a.innerR, e = a.start, f = a.end, a = a.x; return this.symbol("arc", a || 0, b || 0, c || 0, c || 0, { innerR: d || 0, start: e || 0, end: f || 0 }) }, rect: function (a, b, c, d, e, f) { if (kb(a)) b = a.y, c = a.width, d = a.height, e = a.r, f = a.strokeWidth, a = a.x; e = this.createElement("rect").attr({ rx: e, ry: e, fill: ga }); return e.attr(e.crisp(f, a, b, $(c, 0), $(d, 0))) }, setSize: function (a, b, c) { var d = this.alignedObjects, e = d.length; this.width = a; this.height = b; for (this.boxWrapper[p(c, !0) ? "animate" : "attr"]({ width: a, height: b }); e--; ) d[e].align() },
        g: function (a) { var b = this.createElement("g"); return y(a) ? b.attr({ "class": Da + a }) : b }, image: function (a, b, c, d, e) { var f = { preserveAspectRatio: ga }; arguments.length > 1 && N(f, { x: b, y: c, width: d, height: e }); f = this.createElement("image").attr(f); f.element.setAttributeNS ? f.element.setAttributeNS("http://www.w3.org/1999/xlink", "href", a) : f.element.setAttribute("hc-svg-href", a); return f }, symbol: function (a, b, c, d, e, f) {
            var g, h = this.symbols[a], h = h && h(B(b), B(c), d, e, f), i = /^url\((.*?)\)$/, k; if (h) g = this.path(h), N(g, { symbolName: a,
                x: b, y: c, width: d, height: e
            }), f && N(g, f); else if (i.test(a)) { var j = function (a, b) { a.attr({ width: b[0], height: b[1] }).translate(-B(b[0] / 2), -B(b[1] / 2)) }; k = a.match(i)[1]; a = Dc[k]; g = this.image(k).attr({ x: b, y: c }); a ? j(g, a) : (g.attr({ width: 0, height: 0 }), xa("img", { onload: function () { j(g, Dc[k] = [this.width, this.height]) }, src: k })) } return g
        }, symbols: { circle: function (a, b, c, d) { var e = 0.166 * c; return [ua, a + c / 2, b, "C", a + c + e, b, a + c + e, b + d, a + c / 2, b + d, "C", a - e, b + d, a - e, b, a + c / 2, b, "Z"] }, square: function (a, b, c, d) {
            return [ua, a, b, ha, a + c, b, a + c,
b + d, a, b + d, "Z"]
        }, triangle: function (a, b, c, d) { return [ua, a + c / 2, b, ha, a + c, b + d, a, b + d, "Z"] }, "triangle-down": function (a, b, c, d) { return [ua, a, b, ha, a + c, b, a + c / 2, b + d, "Z"] }, diamond: function (a, b, c, d) { return [ua, a + c / 2, b, ha, a + c, b + d / 2, a + c / 2, b + d, a, b + d / 2, "Z"] }, arc: function (a, b, c, d, e) { var f = e.start, c = e.r || c || d, g = e.end - 1.0E-6, d = e.innerR, h = fa(f), i = sa(f), k = fa(g), g = sa(g), e = e.end - f < ab ? 0 : 1; return [ua, a + c * h, b + c * i, "A", c, c, 0, e, 1, a + c * k, b + c * g, ha, a + d * k, b + d * g, "A", d, d, 0, e, 0, a + d * h, b + d * i, "Z"] } 
        }, clipRect: function (a, b, c, d) {
            var e = Da + lc++, f =
this.createElement("clipPath").attr({ id: e }).add(this.defs), a = this.rect(a, b, c, d, 0).add(f); a.Id = e; a.clipPath = f; return a
        }, color: function (a, b, c) {
            var d, e = /^rgba/; if (a && a.linearGradient) {
                var f = this, g = a.linearGradient, b = !zb(g), c = f.gradients, h, i = g.x1 || g[0] || 0, k = g.y1 || g[1] || 0, j = g.x2 || g[2] || 0, m = g.y2 || g[3] || 0, n, u, t = [b, i, k, j, m, a.stops.join(",")].join(","); c[t] ? g = r(c[t].element, "id") : (g = Da + lc++, h = f.createElement("linearGradient").attr(N({ id: g, x1: i, y1: k, x2: j, y2: m }, b ? null : { gradientUnits: "userSpaceOnUse" })).add(f.defs),
h.stops = [], o(a.stops, function (a) { e.test(a[1]) ? (d = bb(a[1]), n = d.get("rgb"), u = d.get("a")) : (n = a[1], u = 1); a = f.createElement("stop").attr({ offset: a[0], "stop-color": n, "stop-opacity": u }).add(h); h.stops.push(a) }), c[t] = h); return "url(" + this.url + "#" + g + ")"
            } else return e.test(a) ? (d = bb(a), r(b, c + "-opacity", d.get("a")), d.get("rgb")) : (b.removeAttribute(c + "-opacity"), a)
        }, text: function (a, b, c, d) {
            var e = Ba.chart.style; if (d && !this.forExport) return this.html(a, b, c); b = B(p(b, 0)); c = B(p(c, 0)); a = this.createElement("text").attr({ x: b,
                y: c, text: a
            }).css({ fontFamily: e.fontFamily, fontSize: e.fontSize }); a.x = b; a.y = c; return a
        }, html: function (a, b, c) {
            var d = Ba.chart.style, e = this.createElement("span"), f = e.attrSetters, g = e.element, h = e.renderer; f.text = function (a) { g.innerHTML = a; return !1 }; f.x = f.y = f.align = function (a, b) { b === "align" && (b = "textAlign"); e[b] = a; e.htmlUpdateTransform(); return !1 }; e.attr({ text: a, x: B(b), y: B(c) }).css({ position: tb, whiteSpace: "nowrap", fontFamily: d.fontFamily, fontSize: d.fontSize }); e.css = e.htmlCss; if (h.isSVG) e.add = function (a) {
                var b,
c, d = h.box.parentNode; if (a) { if (b = a.div, !b) b = a.div = xa(db, { className: r(a.element, "class") }, { position: tb, left: a.attr("translateX") + ia, top: a.attr("translateY") + ia }, d), c = b.style, N(a.attrSetters, { translateX: function (a) { c.left = a + ia }, translateY: function (a) { c.top = a + ia }, visibility: function (a, b) { c[b] = a } }) } else b = d; b.appendChild(g); e.added = !0; e.alignOnAdd && e.htmlUpdateTransform(); return e
            }; return e
        }, label: function (a, b, c, d, e, f, g) {
            function h() {
                var a = m.styles, a = a && a.textAlign, b = x, c = m.element.style, c = x + B(Q(c && c.fontSize ||
11) * 1.2); if (y(E) && (a === "center" || a === "right")) b += { center: 0.5, right: 1}[a] * (E - t.width); (b !== n.x || c !== n.y) && n.attr({ x: b, y: c }); n.x = b; n.y = c
            } function i(a, b) { u ? u.attr(a, b) : r[a] = b } function k() { m.attr({ text: a, x: b, y: c, anchorX: e, anchorY: f }) } var j = this, m = j.g(), n = j.text("", 0, 0, g).attr({ zIndex: 1 }).add(m), u, t, p = "left", x = 3, E, q, w, v, Sa = 0, r = {}, g = m.attrSetters; ja(m, "add", k); g.width = function (a) { E = a; return !1 }; g.height = function (a) { q = a; return !1 }; g.padding = function (a) { x = a; h(); return !1 }; g.align = function (a) { p = a; return !1 }; g.text =
function (a, b) { n.attr(b, a); t = (E === void 0 || q === void 0 || m.styles.textAlign) && n.getBBox(!0); m.width = (E || t.width) + 2 * x; m.height = (q || t.height) + 2 * x; if (!u) m.box = u = d ? j.symbol(d, 0, 0, m.width, m.height) : j.rect(0, 0, m.width, m.height, 0, r["stroke-width"]), u.add(m); u.attr(M({ width: m.width, height: m.height }, r)); r = null; h(); return !1 }; g["stroke-width"] = function (a, b) { Sa = a % 2 / 2; i(b, a); return !1 }; g.stroke = g.fill = g.r = function (a, b) { i(b, a); return !1 }; g.anchorX = function (a, b) { e = a; i(b, a + Sa - w); return !1 }; g.anchorY = function (a, b) {
    f = a; i(b,
a - v); return !1
}; g.x = function (a) { w = a; w -= { left: 0, center: 0.5, right: 1}[p] * ((E || t.width) + x); m.attr("translateX", B(w)); return !1 }; g.y = function (a) { v = a; m.attr("translateY", B(a)); return !1 }; var A = m.css; return N(m, { css: function (a) { if (a) { var b = {}, a = M({}, a); o("fontSize,fontWeight,fontFamily,color,lineHeight,width".split(","), function (c) { a[c] !== ba && (b[c] = a[c], delete a[c]) }); n.css(b) } return A.call(m, a) }, getBBox: function () { return u.getBBox() }, shadow: function (a) { u.shadow(a); return m }, destroy: function () {
    Na(m, "add", k);
    Na(m.element, "mouseenter"); Na(m.element, "mouseleave"); n && (n = n.destroy()); pa.prototype.destroy.call(m)
} 
})
        } 
    }; $b = Fb; var Wa; if (!Qb && !wa) A = { Element: O(pa, { init: function (a, b) { var c = ["<", b, ' filled="f" stroked="f"'], d = ["position: ", tb, ";"]; (b === "shape" || b === db) && d.push("left:0;top:0;width:10px;height:10px;"); wb && d.push("visibility: ", b === db ? $a : eb); c.push(' style="', d.join(""), '"/>'); if (b) c = b === db || b === "span" || b === "img" ? c.join("") : a.prepVML(c), this.element = xa(c); this.renderer = a; this.attrSetters = {} }, add: function (a) {
        var b =
this.renderer, c = this.element, d = b.box, d = a ? a.element || a : d; a && a.inverted && b.invertChild(c, d); wb && d.gVis === $a && U(c, { visibility: $a }); d.appendChild(c); this.added = !0; this.alignOnAdd && !this.deferUpdateTransform && this.htmlUpdateTransform(); Z(this, "add"); return this
    }, toggleChildren: function (a, b) { for (var c = a.childNodes, d = c.length; d--; ) U(c[d], { visibility: b }), c[d].nodeName === "DIV" && this.toggleChildren(c[d], b) }, attr: function (a, b) {
        var c, d, e, f = this.element || {}, g = f.style, h = f.nodeName, i = this.renderer, k = this.symbolName,
j, m = this.shadows, n, o = this.attrSetters, p = this; yb(a) && y(b) && (c = a, a = {}, a[c] = b); if (yb(a)) c = a, p = c === "strokeWidth" || c === "stroke-width" ? this.strokeweight : this[c]; else for (c in a) if (d = a[c], n = !1, e = o[c] && o[c](d, c), e !== !1 && d !== null) {
            e !== ba && (d = e); if (k && /^(x|y|r|start|end|width|height|innerR|anchorX|anchorY)/.test(c)) j || (this.symbolAttr(a), j = !0), n = !0; else if (c === "d") {
                d = d || []; this.d = d.join(" "); e = d.length; for (n = []; e--; ) n[e] = Ja(d[e]) ? B(d[e] * 10) - 5 : d[e] === "Z" ? "x" : d[e]; d = n.join(" ") || "x"; f.path = d; if (m) for (e = m.length; e--; ) m[e].path =
d; n = !0
            } else if (c === "zIndex" || c === "visibility") { if (wb && c === "visibility" && h === "DIV") f.gVis = d, this.toggleChildren(f, d), d === eb && (d = null); d && (g[c] = d); n = !0 } else if (c === "width" || c === "height") d = $(0, d), this[c] = d, this.updateClipping ? (this[c] = d, this.updateClipping()) : g[c] = d, n = !0; else if (c === "x" || c === "y") this[c] = d, g[{ x: "left", y: "top"}[c]] = d; else if (c === "class") f.className = d; else if (c === "stroke") d = i.color(d, f, c), c = "strokecolor"; else if (c === "stroke-width" || c === "strokeWidth") f.stroked = d ? !0 : !1, c = "strokeweight", this[c] =
d, Ja(d) && (d += ia); else if (c === "dashstyle") (f.getElementsByTagName("stroke")[0] || xa(i.prepVML(["<stroke/>"]), null, null, f))[c] = d || "solid", this.dashstyle = d, n = !0; else if (c === "fill") h === "SPAN" ? g.color = d : (f.filled = d !== ga ? !0 : !1, d = i.color(d, f, c), c = "fillcolor"); else if (c === "translateX" || c === "translateY" || c === "rotation") this[c] = d, this.htmlUpdateTransform(), n = !0; else if (c === "text") this.bBox = null, f.innerHTML = d, n = !0; if (m && c === "visibility") for (e = m.length; e--; ) m[e].style[c] = d; n || (wb ? f[c] = d : r(f, c, d))
        } return p
    }, clip: function (a) {
        var b =
this, c = a.members; c.push(b); b.destroyClip = function () { Gb(c, b) }; return b.css(a.getCSS(b.inverted))
    }, css: pa.prototype.htmlCss, safeRemoveChild: function (a) { a.parentNode && Vb(a) }, destroy: function () { this.destroyClip && this.destroyClip(); return pa.prototype.destroy.apply(this) }, empty: function () { for (var a = this.element.childNodes, b = a.length, c; b--; ) c = a[b], c.parentNode.removeChild(c) }, on: function (a, b) { this.element["on" + a] = function () { var a = ea.event; a.target = a.srcElement; b(a) }; return this }, shadow: function (a, b) {
        var c =
[], d, e = this.element, f = this.renderer, g, h = e.style, i, k = e.path; k && typeof k.value !== "string" && (k = "x"); if (a) { for (d = 1; d <= 3; d++) i = ['<shape isShadow="true" strokeweight="', 7 - 2 * d, '" filled="false" path="', k, '" coordsize="100,100" style="', e.style.cssText, '" />'], g = xa(f.prepVML(i), null, { left: Q(h.left) + 1, top: Q(h.top) + 1 }), i = ['<stroke color="black" opacity="', 0.05 * d, '"/>'], xa(f.prepVML(i), null, null, g), b ? b.element.appendChild(g) : e.parentNode.insertBefore(g, e), c.push(g); this.shadows = c } return this
    } 
    }), isIE8: Ha.indexOf("MSIE 8.0") >
-1, init: function (a, b, c) { var d; this.alignedObjects = []; d = this.createElement(db); a.appendChild(d.element); this.box = d.element; this.boxWrapper = d; this.setSize(b, c, !1); if (!V.namespaces.hcv) V.namespaces.add("hcv", "urn:schemas-microsoft-com:vml"), V.createStyleSheet().cssText = "hcv\\:fill, hcv\\:path, hcv\\:shape, hcv\\:stroke{ behavior:url(#default#VML); display: inline-block; } " }, clipRect: function (a, b, c, d) {
    var e = this.createElement(); return N(e, { members: [], left: a, top: b, width: c, height: d, getCSS: function (a) {
        var b =
this.top, c = this.left, d = c + this.width, e = b + this.height, b = { clip: "rect(" + B(a ? c : b) + "px," + B(a ? e : d) + "px," + B(a ? d : e) + "px," + B(a ? b : c) + "px)" }; !a && wb && N(b, { width: d + ia, height: e + ia }); return b
    }, updateClipping: function () { o(e.members, function (a) { a.css(e.getCSS(a.inverted)) }) } 
    })
}, color: function (a, b, c) {
    var d, e = /^rgba/; if (a && a.linearGradient) {
        var f, g, h = a.linearGradient, i = h.x1 || h[0] || 0, k = h.y1 || h[1] || 0, j = h.x2 || h[2] || 0, h = h.y2 || h[3] || 0, m, n, p, t; o(a.stops, function (a, b) {
            e.test(a[1]) ? (d = bb(a[1]), f = d.get("rgb"), g = d.get("a")) : (f =
a[1], g = 1); b ? (p = f, t = g) : (m = f, n = g)
        }); if (c === "fill") a = 90 - oa.atan((h - k) / (j - i)) * 180 / ab, a = ['<fill colors="0% ', m, ",100% ", p, '" angle="', a, '" opacity="', t, '" o:opacity2="', n, '" type="gradient" focus="100%" method="any" />'], xa(this.prepVML(a), null, null, b); else return f
    } else if (e.test(a) && b.tagName !== "IMG") return d = bb(a), a = ["<", c, ' opacity="', d.get("a"), '"/>'], xa(this.prepVML(a), null, null, b), d.get("rgb"); else { b = b.getElementsByTagName(c); if (b.length) b[0].opacity = 1; return a } 
}, prepVML: function (a) {
    var b = this.isIE8,
a = a.join(""); b ? (a = a.replace("/>", ' xmlns="urn:schemas-microsoft-com:vml" />'), a = a.indexOf('style="') === -1 ? a.replace("/>", ' style="display:inline-block;behavior:url(#default#VML);" />') : a.replace('style="', 'style="display:inline-block;behavior:url(#default#VML);')) : a = a.replace("<", "<hcv:"); return a
}, text: Fb.prototype.html, path: function (a) { return this.createElement("shape").attr({ coordsize: "100 100", d: a }) }, circle: function (a, b, c) { return this.symbol("circle").attr({ x: a - c, y: b - c, width: 2 * c, height: 2 * c }) },
        g: function (a) { var b; a && (b = { className: Da + a, "class": Da + a }); return this.createElement(db).attr(b) }, image: function (a, b, c, d, e) { var f = this.createElement("img").attr({ src: a }); arguments.length > 1 && f.css({ left: b, top: c, width: d, height: e }); return f }, rect: function (a, b, c, d, e, f) { if (kb(a)) b = a.y, c = a.width, d = a.height, f = a.strokeWidth, a = a.x; var g = this.symbol("rect"); g.r = e; return g.attr(g.crisp(f, a, b, $(c, 0), $(d, 0))) }, invertChild: function (a, b) { var c = b.style; U(a, { flip: "x", left: Q(c.width) - 10, top: Q(c.height) - 10, rotation: -90 }) },
        symbols: { arc: function (a, b, c, d, e) { var f = e.start, g = e.end, c = e.r || c || d, d = fa(f), h = sa(f), i = fa(g), k = sa(g), e = e.innerR, j = 0.07 / c, m = e && 0.1 / e || 0; if (g - f === 0) return ["x"]; else 2 * ab - g + f < j ? i = -j : g - f < m && (i = fa(f + m)); return ["wa", a - c, b - c, a + c, b + c, a + c * d, b + c * h, a + c * i, b + c * k, "at", a - e, b - e, a + e, b + e, a + e * i, b + e * k, a + e * d, b + e * h, "x", "e"] }, circle: function (a, b, c, d) { return ["wa", a, b, a + c, b + d, a + c, b + d / 2, a + c, b + d / 2, "e"] }, rect: function (a, b, c, d, e) {
            if (!y(e)) return []; var f = a + c, g = b + d, c = Za(e.r || 0, c, d); return [ua, a + c, b, ha, f - c, b, "wa", f - 2 * c, b, f, b + 2 * c, f -
c, b, f, b + c, ha, f, g - c, "wa", f - 2 * c, g - 2 * c, f, g, f, g - c, f - c, g, ha, a + c, g, "wa", a, g - 2 * c, a + 2 * c, g, a + c, g, a, g - c, ha, a, b + c, "wa", a, b, a + 2 * c, b + 2 * c, a, b + c, a + c, b, "x", "e"]
        } 
        }
    }, Wa = function () { this.init.apply(this, arguments) }, Wa.prototype = M(Fb.prototype, A), $b = Wa; var nc, Ac; wa && (nc = function () { }, Ac = function () { function a() { var a = b.length, d; for (d = 0; d < a; d++) b[d](); b = [] } var b = []; return { push: function (c, d) { b.length === 0 && Fc(d, a); b.push(c) } } } ()); $b = Wa || nc || Fb; vc.prototype.callbacks = []; var Aa = function () { }; Aa.prototype = { init: function (a, b, c) {
        var d =
a.chart.counters; this.series = a; this.applyOptions(b, c); this.pointAttr = {}; if (a.options.colorByPoint) { b = a.chart.options.colors; if (!this.options) this.options = {}; this.color = this.options.color = this.color || b[d.color++]; d.wrapColor(b.length) } a.chart.pointCount++; return this
    }, applyOptions: function (a, b) {
        var c = this.series, d = typeof a; this.config = a; if (d === "number" || a === null) this.y = a; else if (typeof a[0] === "number") this.x = a[0], this.y = a[1]; else if (d === "object" && typeof a.length !== "number") {
            if (N(this, a), this.options =
a, a.dataLabels) c._hasPointLabels = !0
        } else if (typeof a[0] === "string") this.name = a[0], this.y = a[1]; if (this.x === ba) this.x = b === ba ? c.autoIncrement() : b
    }, destroy: function () { var a = this.series, b = a.chart.hoverPoints, c; a.chart.pointCount--; b && (this.setState(), Gb(b, this)); if (this === a.chart.hoverPoint) this.onMouseOut(); a.chart.hoverPoints = null; if (this.graphic || this.dataLabel) Na(this), this.destroyElements(); this.legendItem && this.series.chart.legend.destroyItem(this); for (c in this) this[c] = null }, destroyElements: function () {
        for (var a =
"graphic,tracker,dataLabel,group,connector,shadowGroup".split(","), b, c = 6; c--; ) b = a[c], this[b] && (this[b] = this[b].destroy())
    }, getLabelConfig: function () { return { x: this.category, y: this.y, key: this.name || this.category, series: this.series, point: this, percentage: this.percentage, total: this.total || this.stackTotal} }, select: function (a, b) {
        var c = this, d = c.series.chart, a = p(a, !c.selected); c.firePointEvent(a ? "select" : "unselect", { accumulate: b }, function () {
            c.selected = a; c.setState(a && "select"); b || o(d.getSelectedPoints(), function (a) {
                if (a.selected &&
a !== c) a.selected = !1, a.setState(Ga), a.firePointEvent("unselect")
            })
        })
    }, onMouseOver: function () { var a = this.series, b = a.chart, c = b.tooltip, d = b.hoverPoint; if (d && d !== this) d.onMouseOut(); this.firePointEvent("mouseOver"); c && (!c.shared || a.noSharedTooltip) && c.refresh(this); this.setState(Ra); b.hoverPoint = this }, onMouseOut: function () { this.firePointEvent("mouseOut"); this.setState(); this.series.chart.hoverPoint = null }, tooltipFormatter: function (a) {
        var b = this.series, c = b.tooltipOptions, d = String(this.y).split("."), d = d[1] ?
d[1].length : 0, e = a.match(/\{(series|point)\.[a-zA-Z]+\}/g), f = /[\.}]/, g, h, i; for (i in e) h = e[i], yb(h) && h !== a && (g = h.indexOf("point") === 1 ? this : b, g = h === "{point.y}" ? (c.valuePrefix || c.yPrefix || "") + fc(this.y, p(c.valueDecimals, c.yDecimals, d)) + (c.valueSuffix || c.ySuffix || "") : g[e[i].split(f)[1]], a = a.replace(e[i], g)); return a
    }, update: function (a, b, c) {
        var d = this, e = d.series, f = d.graphic, g, h = e.data, i = h.length, k = e.chart, b = p(b, !0); d.firePointEvent("update", { options: a }, function () {
            d.applyOptions(a); kb(a) && (e.getAttribs(),
f && f.attr(d.pointAttr[e.state])); for (g = 0; g < i; g++) if (h[g] === d) { e.xData[g] = d.x; e.yData[g] = d.y; e.options.data[g] = a; break } e.isDirty = !0; e.isDirtyData = !0; b && k.redraw(c)
        })
    }, remove: function (a, b) { var c = this, d = c.series, e = d.chart, f, g = d.data, h = g.length; Lb(b, e); a = p(a, !0); c.firePointEvent("remove", null, function () { for (f = 0; f < h; f++) if (g[f] === c) { g.splice(f, 1); d.options.data.splice(f, 1); d.xData.splice(f, 1); d.yData.splice(f, 1); break } c.destroy(); d.isDirty = !0; d.isDirtyData = !0; a && e.redraw() }) }, firePointEvent: function (a,
b, c) { var d = this, e = this.series.options; (e.point.events[a] || d.options && d.options.events && d.options.events[a]) && this.importEvents(); a === "click" && e.allowPointSelect && (c = function (a) { d.select(null, a.ctrlKey || a.metaKey || a.shiftKey) }); Z(this, a, b, c) }, importEvents: function () { if (!this.hasImportedEvents) { var a = M(this.series.options.point, this.options).events, b; this.events = a; for (b in a) ja(this, b, a[b]); this.hasImportedEvents = !0 } }, setState: function (a) {
    var b = this.plotX, c = this.plotY, d = this.series, e = d.options.states,
f = ka[d.type].marker && d.options.marker, g = f && !f.enabled, h = f && f.states[a], i = h && h.enabled === !1, k = d.stateMarkerGraphic, j = d.chart, m = this.pointAttr, a = a || Ga; if (!(a === this.state || this.selected && a !== "select" || e[a] && e[a].enabled === !1 || a && (i || g && !h.enabled))) {
        if (this.graphic) e = this.graphic.symbolName && m[a].r, this.graphic.attr(M(m[a], e ? { x: b - e, y: c - e, width: 2 * e, height: 2 * e} : {})); else {
            if (a) { if (!k) e = f.radius, d.stateMarkerGraphic = k = j.renderer.symbol(d.symbol, -e, -e, 2 * e, 2 * e).attr(m[a]).add(d.group); k.translate(b, c) } if (k) k[a ?
"show" : "hide"]()
        } this.state = a
    } 
} 
    }; var R = function () { }; R.prototype = { isCartesian: !0, type: "line", pointClass: Aa, pointAttrToOptions: { stroke: "lineColor", "stroke-width": "lineWidth", fill: "fillColor", r: "radius" }, init: function (a, b) {
        var c, d; d = a.series.length; this.chart = a; this.options = b = this.setOptions(b); this.bindAxes(); N(this, { index: d, name: b.name || "Series " + (d + 1), state: Ga, pointAttr: {}, visible: b.visible !== !1, selected: b.selected === !0 }); if (wa) b.animation = !1; d = b.events; for (c in d) ja(this, c, d[c]); if (d && d.click || b.point &&
b.point.events && b.point.events.click || b.allowPointSelect) a.runTrackerClick = !0; this.getColor(); this.getSymbol(); this.setData(b.data, !1)
    }, bindAxes: function () { var a = this, b = a.options, c = a.chart, d; a.isCartesian && o(["xAxis", "yAxis"], function (e) { o(c[e], function (c) { d = c.options; if (b[e] === d.index || b[e] === ba && d.index === 0) c.series.push(a), a[e] = c, c.isDirty = !0 }) }) }, autoIncrement: function () {
        var a = this.options, b = this.xIncrement, b = p(b, a.pointStart, 0); this.pointInterval = p(this.pointInterval, a.pointInterval, 1); this.xIncrement =
b + this.pointInterval; return b
    }, getSegments: function () { var a = -1, b = [], c, d = this.points, e = d.length; if (e) if (this.options.connectNulls) { for (c = e; c--; ) d[c].y === null && d.splice(c, 1); b = [d] } else o(d, function (c, g) { c.y === null ? (g > a + 1 && b.push(d.slice(a + 1, g)), a = g) : g === e - 1 && b.push(d.slice(a + 1, g + 1)) }); this.segments = b }, setOptions: function (a) { var b = this.chart.options, c = b.plotOptions, d = a.data; a.data = null; c = M(c[this.type], c.series, a); c.data = a.data = d; this.tooltipOptions = M(b.tooltip, c.tooltip); return c }, getColor: function () {
        var a =
this.chart.options.colors, b = this.chart.counters; this.color = this.options.color || a[b.color++] || "#0000ff"; b.wrapColor(a.length)
    }, getSymbol: function () { var a = this.options.marker, b = this.chart, c = b.options.symbols, b = b.counters; this.symbol = a.symbol || c[b.symbol++]; if (/^url/.test(this.symbol)) a.radius = 0; b.wrapSymbol(c.length) }, addPoint: function (a, b, c, d) {
        var e = this.data, f = this.graph, g = this.area, h = this.chart, i = this.xData, k = this.yData, j = f && f.shift || 0, m = this.options.data; Lb(d, h); if (f && c) f.shift = j + 1; if (g) g.shift =
j + 1, g.isArea = !0; b = p(b, !0); d = { series: this }; this.pointClass.prototype.applyOptions.apply(d, [a]); i.push(d.x); k.push(this.valueCount === 4 ? [d.open, d.high, d.low, d.close] : d.y); m.push(a); c && (e[0] ? e[0].remove(!1) : (e.shift(), i.shift(), k.shift(), m.shift())); this.getAttribs(); this.isDirtyData = this.isDirty = !0; b && h.redraw()
    }, setData: function (a, b) {
        var c = this.points, d = this.options, e = this.initialColor, f = this.chart, g = null; this.xIncrement = null; this.pointRange = this.xAxis && this.xAxis.categories && 1 || d.pointRange; if (y(e)) f.counters.color =
e; var h = [], i = [], k = a ? a.length : [], j = this.valueCount === 4; if (k > (d.turboThreshold || 1E3)) { for (e = 0; g === null && e < k; ) g = a[e], e++; if (Ja(g)) { g = p(d.pointStart, 0); d = p(d.pointInterval, 1); for (e = 0; e < k; e++) h[e] = g, i[e] = a[e], g += d; this.xIncrement = g } else if (zb(g)) if (j) for (e = 0; e < k; e++) d = a[e], h[e] = d[0], i[e] = d.slice(1, 5); else for (e = 0; e < k; e++) d = a[e], h[e] = d[0], i[e] = d[1] } else for (e = 0; e < k; e++) d = { series: this }, this.pointClass.prototype.applyOptions.apply(d, [a[e]]), h[e] = d.x, i[e] = j ? [d.open, d.high, d.low, d.close] : d.y; this.data = []; this.options.data =
a; this.xData = h; this.yData = i; for (e = c && c.length || 0; e--; ) c[e] && c[e].destroy && c[e].destroy(); this.isDirty = this.isDirtyData = f.isDirtyBox = !0; p(b, !0) && f.redraw(!1)
    }, remove: function (a, b) { var c = this, d = c.chart, a = p(a, !0); if (!c.isRemoving) c.isRemoving = !0, Z(c, "remove", null, function () { c.destroy(); d.isDirtyLegend = d.isDirtyBox = !0; a && d.redraw(b) }); c.isRemoving = !1 }, processData: function (a) {
        var b = this.xData, c = this.yData, d = b.length, e = 0, f = d, g, h, i = this.xAxis, k = this.options, j = k.cropThreshold; if (this.isCartesian && !this.isDirty &&
!i.isDirty && !this.yAxis.isDirty && !a) return !1; if (!j || d > j || this.forceCrop) if (a = i.getExtremes(), i = a.min, j = a.max, b[d - 1] < i || b[0] > j) b = [], c = []; else if (b[0] < i || b[d - 1] > j) { for (a = 0; a < d; a++) if (b[a] >= i) { e = $(0, a - 1); break } for (; a < d; a++) if (b[a] > j) { f = a + 1; break } b = b.slice(e, f); c = c.slice(e, f); g = !0 } for (a = b.length - 1; a > 0; a--) if (d = b[a] - b[a - 1], h === ba || d < h) h = d; this.cropped = g; this.cropStart = e; this.processedXData = b; this.processedYData = c; if (k.pointRange === null) this.pointRange = h || 1; this.closestPointRange = h
    }, generatePoints: function () {
        var a =
this.options.data, b = this.data, c, d = this.processedXData, e = this.processedYData, f = this.pointClass, g = d.length, h = this.cropStart || 0, i, k = this.hasGroupedData, j, m = [], n; if (!b && !k) b = [], b.length = a.length, b = this.data = b; for (n = 0; n < g; n++) i = h + n, k ? m[n] = (new f).init(this, [d[n]].concat(Hb(e[n]))) : (b[i] ? j = b[i] : b[i] = j = (new f).init(this, a[i], d[n]), m[n] = j); if (b && (g !== (c = b.length) || k)) for (n = 0; n < c; n++) n === h && !k && (n += g), b[n] && b[n].destroyElements(); this.data = b; this.points = m
    }, translate: function () {
        this.processedXData || this.processData();
        this.generatePoints(); for (var a = this.chart, b = this.options, c = b.stacking, d = this.xAxis, e = d.categories, f = this.yAxis, g = this.points, h = g.length, i = !!this.modifyValue, k, j = f.series, m = j.length; m--; ) if (j[m].visible) { m === this.index && (k = !0); break } for (m = 0; m < h; m++) {
            var j = g[m], n = j.x, o = j.y, p = j.low, D = f.stacks[(o < b.threshold ? "-" : "") + this.stackKey]; j.plotX = B(d.translate(n, 0, 0, 0, 1) * 10) / 10; if (c && this.visible && D && D[n]) {
                p = D[n]; n = p.total; p.cum = p = p.cum - o; o = p + o; if (k) p = b.threshold; c === "percent" && (p = n ? p * 100 / n : 0, o = n ? o * 100 / n : 0);
                j.percentage = n ? j.y * 100 / n : 0; j.stackTotal = n
            } j.yBottom = y(p) ? f.translate(p, 0, 1, 0, 1) : null; i && (o = this.modifyValue(o, j)); if (o !== null) j.plotY = B(f.translate(o, 0, 1, 0, 1) * 10) / 10; j.clientX = a.inverted ? a.plotHeight - j.plotX : j.plotX; j.category = e && e[j.x] !== ba ? e[j.x] : j.x
        } this.getSegments()
    }, setTooltipPoints: function (a) {
        var b = this.chart, c = b.inverted, d = [], b = B((c ? b.plotTop : b.plotLeft) + b.plotSizeX), e, f; e = this.xAxis; var g, h, i = []; if (this.options.enableMouseTracking !== !1) {
            if (a) this.tooltipPoints = null; o(this.segments || this.points,
function (a) { d = d.concat(a) }); e && e.reversed && (d = d.reverse()); a = d.length; for (h = 0; h < a; h++) { g = d[h]; e = d[h - 1] ? d[h - 1]._high + 1 : 0; for (f = g._high = d[h + 1] ? Ta((g.plotX + (d[h + 1] ? d[h + 1].plotX : b)) / 2) : b; e <= f; ) i[c ? b - e++ : e++] = g } this.tooltipPoints = i
        } 
    }, tooltipHeaderFormatter: function (a) { var b = this.tooltipOptions, c = b.xDateFormat || "%A, %b %e, %Y", d = this.xAxis; return b.headerFormat.replace("{point.key}", d && d.options.type === "datetime" ? dc(c, a) : a).replace("{series.name}", this.name).replace("{series.color}", this.color) }, onMouseOver: function () {
        var a =
this.chart, b = a.hoverSeries; if (ra || !a.mouseIsDown) { if (b && b !== this) b.onMouseOut(); this.options.events.mouseOver && Z(this, "mouseOver"); this.setState(Ra); a.hoverSeries = this } 
    }, onMouseOut: function () { var a = this.options, b = this.chart, c = b.tooltip, d = b.hoverPoint; if (d) d.onMouseOut(); this && a.events.mouseOut && Z(this, "mouseOut"); c && !a.stickyTracking && !c.shared && c.hide(); this.setState(); b.hoverSeries = null }, animate: function (a) {
        var b = this.chart, c = this.clipRect, d = this.options.animation; d && !kb(d) && (d = {}); if (a) {
            if (!c.isAnimating) c.attr("width",
0), c.isAnimating = !0
        } else c.animate({ width: b.plotSizeX }, d), this.animate = null
    }, drawPoints: function () { var a, b = this.points, c = this.chart, d, e, f, g, h, i, k, j; if (this.options.marker.enabled) for (f = b.length; f--; ) if (g = b[f], d = g.plotX, e = g.plotY, j = g.graphic, e !== ba && !isNaN(e)) if (a = g.pointAttr[g.selected ? "select" : Ga], h = a.r, i = p(g.marker && g.marker.symbol, this.symbol), k = i.indexOf("url") === 0, j) j.animate(N({ x: d - h, y: e - h }, j.symbolName ? { width: 2 * h, height: 2 * h} : {})); else if (h > 0 || k) g.graphic = c.renderer.symbol(i, d - h, e - h, 2 * h, 2 * h).attr(a).add(this.group) },
        convertAttribs: function (a, b, c, d) { var e = this.pointAttrToOptions, f, g, h = {}, a = a || {}, b = b || {}, c = c || {}, d = d || {}; for (f in e) g = e[f], h[f] = p(a[g], b[f], c[f], d[f]); return h }, getAttribs: function () {
            var a = this, b = ka[a.type].marker ? a.options.marker : a.options, c = b.states, d = c[Ra], e, f = a.color, g = { stroke: f, fill: f }, h = a.points, i = [], k, j = a.pointAttrToOptions, m; a.options.marker ? (d.radius = d.radius || b.radius + 2, d.lineWidth = d.lineWidth || b.lineWidth + 1) : d.color = d.color || bb(d.color || f).brighten(d.brightness).get(); i[Ga] = a.convertAttribs(b,
g); o([Ra, "select"], function (b) { i[b] = a.convertAttribs(c[b], i[Ga]) }); a.pointAttr = i; for (f = h.length; f--; ) {
                g = h[f]; if ((b = g.options && g.options.marker || g.options) && b.enabled === !1) b.radius = 0; e = !1; if (g.options) for (m in j) y(b[j[m]]) && (e = !0); if (e) { k = []; c = b.states || {}; e = c[Ra] = c[Ra] || {}; if (!a.options.marker) e.color = bb(e.color || g.options.color).brighten(e.brightness || d.brightness).get(); k[Ga] = a.convertAttribs(b, i[Ga]); k[Ra] = a.convertAttribs(c[Ra], i[Ra], k[Ga]); k.select = a.convertAttribs(c.select, i.select, k[Ga]) } else k =
i; g.pointAttr = k
            } 
        }, destroy: function () {
            var a = this, b = a.chart, c = a.clipRect, d = /AppleWebKit\/533/.test(Ha), e, f, g = a.data || [], h, i, k; Z(a, "destroy"); Na(a); o(["xAxis", "yAxis"], function (b) { if (k = a[b]) Gb(k.series, a), k.isDirty = !0 }); a.legendItem && a.chart.legend.destroyItem(a); for (f = g.length; f--; ) (h = g[f]) && h.destroy && h.destroy(); a.points = null; if (c && c !== b.clipRect) a.clipRect = c.destroy(); o(["area", "graph", "dataLabelsGroup", "group", "tracker"], function (b) { a[b] && (e = d && b === "group" ? "hide" : "destroy", a[b][e]()) }); if (b.hoverSeries ===
a) b.hoverSeries = null; Gb(b.series, a); for (i in a) delete a[i]
        }, drawDataLabels: function () {
            var a = this, b = a.options, c = b.dataLabels; if (c.enabled || a._hasPointLabels) {
                var d, e, f = a.points, g, h, i, k = a.dataLabelsGroup, j = a.chart, m = a.xAxis, m = m ? m.left : j.plotLeft, n = a.yAxis, n = n ? n.top : j.plotTop, u = j.renderer, t = j.inverted, D = a.type, x = b.stacking, E = D === "column" || D === "bar", q = c.verticalAlign === null, w = c.y === null, v; E && (x ? (q && (c = M(c, { verticalAlign: "middle" })), w && (c = M(c, { y: { top: 14, middle: 4, bottom: -6}[c.verticalAlign] }))) : q && (c = M(c,
{ verticalAlign: "top" }))); k ? k.translate(m, n) : k = a.dataLabelsGroup = u.g("data-labels").attr({ visibility: a.visible ? eb : $a, zIndex: 6 }).translate(m, n).add(); h = c; o(f, function (f) {
    v = f.dataLabel; c = h; (g = f.options) && g.dataLabels && (c = M(c, g.dataLabels)); if (v && a.isCartesian && !j.isInsidePlot(f.plotX, f.plotY)) f.dataLabel = v.destroy(); else if (c.enabled) {
        i = c.formatter.call(f.getLabelConfig(), c); var n = f.barX, m = n && n + f.barW / 2 || f.plotX || -999, o = p(f.plotY, -999), q = c.align, r = w ? f.y >= 0 ? -6 : 12 : c.y; d = (t ? j.plotWidth - o : m) + c.x; e = (t ? j.plotHeight -
m : o) + r; D === "column" && (d += { left: -1, right: 1}[q] * f.barW / 2 || 0); !x && t && f.y < 0 && (q = "right", d -= 10); c.style.color = p(c.color, c.style.color, a.color, "black"); if (v) t && !c.y && (e = e + Q(v.styles.lineHeight) * 0.9 - v.getBBox().height / 2), v.attr({ text: i }).animate({ x: d, y: e }); else if (y(i)) v = f.dataLabel = u.text(i, d, e, c.useHTML).attr({ align: q, rotation: c.rotation, zIndex: 1 }).css(c.style).add(k), t && !c.y && v.attr({ y: e + Q(v.styles.lineHeight) * 0.9 - v.getBBox().height / 2 }); if (E && b.stacking && v) m = f.barY, o = f.barW, f = f.barH, v.align(c, null, { x: t ?
j.plotWidth - m - f : n, y: t ? j.plotHeight - n - o : m, width: t ? f : o, height: t ? o : f
})
    } 
})
            } 
        }, drawGraph: function () {
            var a = this, b = a.options, c = a.graph, d = [], e, f = a.area, g = a.group, h = b.lineColor || a.color, i = b.lineWidth, k = b.dashStyle, j, m = a.chart.renderer, n = a.yAxis.getThreshold(b.threshold), u = /^area/.test(a.type), t = [], D = []; o(a.segments, function (c) {
                j = []; o(c, function (d, e) { a.getPointSpline ? j.push.apply(j, a.getPointSpline(c, d, e)) : (j.push(e ? ha : ua), e && b.step && j.push(d.plotX, c[e - 1].plotY), j.push(d.plotX, d.plotY)) }); c.length > 1 ? d = d.concat(j) :
t.push(c[0]); if (u) { var e = [], f, g = j.length; for (f = 0; f < g; f++) e.push(j[f]); g === 3 && e.push(ha, j[1], j[2]); if (b.stacking && a.type !== "areaspline") for (f = c.length - 1; f >= 0; f--) f < c.length - 1 && b.step && e.push(c[f + 1].plotX, c[f].yBottom), e.push(c[f].plotX, c[f].yBottom); else e.push(ha, c[c.length - 1].plotX, n, ha, c[0].plotX, n); D = D.concat(e) } 
            }); a.graphPath = d; a.singlePoints = t; if (u) e = p(b.fillColor, bb(a.color).setOpacity(b.fillOpacity || 0.75).get()), f ? f.animate({ d: D }) : a.area = a.chart.renderer.path(D).attr({ fill: e }).add(g); if (c) Pb(c),
c.animate({ d: d }); else if (i) { c = { stroke: h, "stroke-width": i }; if (k) c.dashstyle = k; a.graph = m.path(d).attr(c).add(g).shadow(b.shadow) } 
        }, render: function () {
            var a = this, b = a.chart, c, d, e = a.options, f = e.clip !== !1, g = e.animation, h = g && a.animate, g = h ? g && g.duration || 500 : 0, i = a.clipRect, k = b.renderer; if (!i && (i = a.clipRect = !b.hasRendered && b.clipRect ? b.clipRect : k.clipRect(0, 0, b.plotSizeX, b.plotSizeY + 1), !b.clipRect)) b.clipRect = i; if (!a.group) c = a.group = k.g("series"), b.inverted && (d = function () { c.attr({ width: b.plotWidth, height: b.plotHeight }).invert() },
d(), ja(b, "resize", d), ja(a, "destroy", function () { Na(b, "resize", d) })), f && c.clip(i), c.attr({ visibility: a.visible ? eb : $a, zIndex: e.zIndex }).translate(a.xAxis.left, a.yAxis.top).add(b.seriesGroup); a.drawDataLabels(); h && a.animate(!0); a.getAttribs(); a.drawGraph && a.drawGraph(); a.drawPoints(); a.options.enableMouseTracking !== !1 && a.drawTracker(); h && a.animate(); setTimeout(function () { i.isAnimating = !1; if ((c = a.group) && i !== b.clipRect && i.renderer) { if (f) c.clip(a.clipRect = b.clipRect); i.destroy() } }, g); a.isDirty = a.isDirtyData =
!1
        }, redraw: function () { var a = this.chart, b = this.isDirtyData, c = this.group; c && (a.inverted && c.attr({ width: a.plotWidth, height: a.plotHeight }), c.animate({ translateX: this.xAxis.left, translateY: this.yAxis.top })); this.translate(); this.setTooltipPoints(!0); this.render(); b && Z(this, "updatedData") }, setState: function (a) {
            var b = this.options, c = this.graph, d = b.states, b = b.lineWidth, a = a || Ga; if (this.state !== a) this.state = a, d[a] && d[a].enabled === !1 || (a && (b = d[a].lineWidth || b + 1), c && !c.dashstyle && c.attr({ "stroke-width": b }, a ? 0 :
500))
        }, setVisible: function (a, b) {
            var c = this.chart, d = this.legendItem, e = this.group, f = this.tracker, g = this.dataLabelsGroup, h, i = this.points, k = c.options.chart.ignoreHiddenSeries; h = this.visible; h = (this.visible = a = a === ba ? !h : a) ? "show" : "hide"; if (e) e[h](); if (f) f[h](); else if (i) for (e = i.length; e--; ) if (f = i[e], f.tracker) f.tracker[h](); if (g) g[h](); d && c.legend.colorizeItem(this, a); this.isDirty = !0; this.options.stacking && o(c.series, function (a) { if (a.options.stacking && a.visible) a.isDirty = !0 }); if (k) c.isDirtyBox = !0; b !== !1 &&
c.redraw(); Z(this, h)
        }, show: function () { this.setVisible(!0) }, hide: function () { this.setVisible(!1) }, select: function (a) { this.selected = a = a === ba ? !this.selected : a; if (this.checkbox) this.checkbox.checked = a; Z(this, a ? "select" : "unselect") }, drawTracker: function () {
            var a = this, b = a.options, c = [].concat(a.graphPath), d = c.length, e = a.chart, f = e.renderer, g = e.options.tooltip.snap, h = a.tracker, i = b.cursor, i = i && { cursor: i }, k = a.singlePoints, j; if (d) for (j = d + 1; j--; ) c[j] === ua && c.splice(j + 1, 0, c[j + 1] - g, c[j + 2], ha), (j && c[j] === ua || j === d) &&
c.splice(j, 0, ha, c[j - 2] + g, c[j - 1]); for (j = 0; j < k.length; j++) d = k[j], c.push(ua, d.plotX - g, d.plotY, ha, d.plotX + g, d.plotY); h ? h.attr({ d: c }) : (h = f.g().clip(e.clipRect).add(e.trackerGroup), a.tracker = f.path(c).attr({ isTracker: !0, stroke: Ec, fill: ga, "stroke-linejoin": "bevel", "stroke-width": b.lineWidth + 2 * g, visibility: a.visible ? eb : $a, zIndex: b.zIndex || 1 }).on(ra ? "touchstart" : "mouseover", function () { if (e.hoverSeries !== a) a.onMouseOver() }).on("mouseout", function () { if (!b.stickyTracking) a.onMouseOut() }).css(i).add(h))
        } 
    }; A = O(R);
    Ca.line = A; A = O(R, { type: "area" }); Ca.area = A; A = O(R, { type: "spline", getPointSpline: function (a, b, c) {
        var d = b.plotX, e = b.plotY, f = a[c - 1], g = a[c + 1], h, i, k, j; if (c && c < a.length - 1) { a = f.plotY; k = g.plotX; var g = g.plotY, m; h = (1.5 * d + f.plotX) / 2.5; i = (1.5 * e + a) / 2.5; k = (1.5 * d + k) / 2.5; j = (1.5 * e + g) / 2.5; m = (j - i) * (k - d) / (k - h) + e - j; i += m; j += m; i > a && i > e ? (i = $(a, e), j = 2 * e - i) : i < a && i < e && (i = Za(a, e), j = 2 * e - i); j > g && j > e ? (j = $(g, e), i = 2 * e - j) : j < g && j < e && (j = Za(g, e), i = 2 * e - j); b.rightContX = k; b.rightContY = j } c ? (b = ["C", f.rightContX || f.plotX, f.rightContY || f.plotY,
h || d, i || e, d, e], f.rightContX = f.rightContY = null) : b = [ua, d, e]; return b
    } 
    }); Ca.spline = A; A = O(A, { type: "areaspline" }); Ca.areaspline = A; var xb = O(R, { type: "column", tooltipOutsidePlot: !0, pointAttrToOptions: { stroke: "borderColor", "stroke-width": "borderWidth", fill: "color", r: "borderRadius" }, init: function () { R.prototype.init.apply(this, arguments); var a = this, b = a.chart; b.hasRendered && o(b.series, function (b) { if (b.type === a.type) b.isDirty = !0 }) }, translate: function () {
        var a = this, b = a.chart, c = a.options, d = c.stacking, e = c.borderWidth,
f = 0, g = a.xAxis, h = g.reversed, i = {}, k, j; R.prototype.translate.apply(a); o(b.series, function (b) { if (b.type === a.type && b.visible && a.options.group === b.options.group) b.options.stacking ? (k = b.stackKey, i[k] === ba && (i[k] = f++), j = i[k]) : j = f++, b.columnIndex = j }); var m = a.points, g = ya(g.translationSlope) * (g.ordinalSlope || g.closestPointRange || 1), n = g * c.groupPadding, u = (g - 2 * n) / f, t = c.pointWidth, D = y(t) ? (u - t) / 2 : u * c.pointPadding, x = ac($(p(t, u - 2 * D), 1)), r = D + (n + ((h ? f - a.columnIndex : a.columnIndex) || 0) * u - g / 2) * (h ? -1 : 1), q = a.yAxis.getThreshold(c.threshold),
w = p(c.minPointLength, 5); o(m, function (f) { var g = f.plotY, h = f.yBottom || q, i = f.plotX + r, j = ac(Za(g, h)), k = ac($(g, h) - j), m = a.yAxis.stacks[(f.y < 0 ? "-" : "") + a.stackKey]; d && a.visible && m && m[f.x] && m[f.x].setOffset(r, x); ya(k) < w && w && (k = w, j = ya(j - q) > w ? h - w : q - (g <= q ? w : 0)); N(f, { barX: i, barY: j, barW: x, barH: k }); f.shapeType = "rect"; g = N(b.renderer.Element.prototype.crisp.apply({}, [e, i, j, x, k]), { r: c.borderRadius }); e % 2 && (g.y -= 1, g.height += 1); f.shapeArgs = g; f.trackerArgs = ya(k) < 3 && M(f.shapeArgs, { height: 6, y: j - 3 }) })
    }, getSymbol: function () { },
        drawGraph: function () { }, drawPoints: function () { var a = this, b = a.options, c = a.chart.renderer, d, e; o(a.points, function (f) { var g = f.plotY; if (g !== ba && !isNaN(g) && f.y !== null) d = f.graphic, e = f.shapeArgs, d ? (Pb(d), d.animate(e)) : f.graphic = d = c[f.shapeType](e).attr(f.pointAttr[f.selected ? "select" : Ga]).add(a.group).shadow(b.shadow) }) }, drawTracker: function () {
            var a = this, b = a.chart, c = b.renderer, d, e, f = +new Date, g = a.options, h = g.cursor, i = h && { cursor: h }, k, j; a.isCartesian && (k = c.g().clip(b.clipRect).add(b.trackerGroup)); o(a.points,
function (h) { e = h.tracker; d = h.trackerArgs || h.shapeArgs; delete d.strokeWidth; if (h.y !== null) e ? e.attr(d) : h.tracker = c[h.shapeType](d).attr({ isTracker: f, fill: Ec, visibility: a.visible ? eb : $a, zIndex: g.zIndex || 1 }).on(ra ? "touchstart" : "mouseover", function (c) { j = c.relatedTarget || c.fromElement; if (b.hoverSeries !== a && r(j, "isTracker") !== f) a.onMouseOver(); h.onMouseOver() }).on("mouseout", function (b) { if (!g.stickyTracking && (j = b.relatedTarget || b.toElement, r(j, "isTracker") !== f)) a.onMouseOut() }).css(i).add(h.group || k) })
        }, animate: function (a) {
            var b =
this, c = b.points, d = b.options; if (!a) o(c, function (a) { var c = a.graphic, a = a.shapeArgs, g = b.yAxis, h = d.threshold; c && (c.attr({ height: 0, y: y(h) ? g.getThreshold(h) : g.translate(g.getExtremes().min, 0, 1, 0, 1) }), c.animate({ height: a.height, y: a.y }, d.animation)) }), b.animate = null
        }, remove: function () { var a = this, b = a.chart; b.hasRendered && o(b.series, function (b) { if (b.type === a.type) b.isDirty = !0 }); R.prototype.remove.apply(a, arguments) } 
    }); Ca.column = xb; A = O(xb, { type: "bar", init: function () {
        this.inverted = !0; xb.prototype.init.apply(this,
arguments)
    } 
    }); Ca.bar = A; A = O(R, { type: "scatter", translate: function () { var a = this; R.prototype.translate.apply(a); o(a.points, function (b) { b.shapeType = "circle"; b.shapeArgs = { x: b.plotX, y: b.plotY, r: a.chart.options.tooltip.snap} }) }, drawTracker: function () {
        for (var a = this, b = a.options.cursor, b = b && { cursor: b }, c = a.points, d = c.length, e; d--; ) if (e = c[d].graphic) e.element._index = d; a._hasTracking ? a._hasTracking = !0 : a.group.on(ra ? "touchstart" : "mouseover", function (b) { a.onMouseOver(); c[b.target._index].onMouseOver() }).on("mouseout",
function () { if (!a.options.stickyTracking) a.onMouseOut() }).css(b)
    } 
    }); Ca.scatter = A; A = O(Aa, { init: function () { Aa.prototype.init.apply(this, arguments); var a = this, b; N(a, { visible: a.visible !== !1, name: p(a.name, "Slice") }); b = function () { a.slice() }; ja(a, "select", b); ja(a, "unselect", b); return a }, setVisible: function (a) {
        var b = this.series.chart, c = this.tracker, d = this.dataLabel, e = this.connector, f = this.shadowGroup, g; g = (this.visible = a = a === ba ? !this.visible : a) ? "show" : "hide"; this.group[g](); if (c) c[g](); if (d) d[g](); if (e) e[g]();
        if (f) f[g](); this.legendItem && b.legend.colorizeItem(this, a)
    }, slice: function (a, b, c) { var d = this.series.chart, e = this.slicedTranslation; Lb(c, d); p(b, !0); a = this.sliced = y(a) ? a : !this.sliced; a = { translateX: a ? e[0] : d.plotLeft, translateY: a ? e[1] : d.plotTop }; this.group.animate(a); this.shadowGroup && this.shadowGroup.animate(a) } 
    }); A = O(R, { type: "pie", isCartesian: !1, pointClass: A, pointAttrToOptions: { stroke: "borderColor", "stroke-width": "borderWidth", fill: "color" }, getColor: function () { this.initialColor = this.chart.counters.color },
        animate: function () { var a = this; o(a.points, function (b) { var c = b.graphic, b = b.shapeArgs, d = -ab / 2; c && (c.attr({ r: 0, start: d, end: d }), c.animate({ r: b.r, start: b.start, end: b.end }, a.options.animation)) }); a.animate = null }, setData: function () { R.prototype.setData.apply(this, arguments); this.processData(); this.generatePoints() }, translate: function () {
            this.generatePoints(); var a = 0, b = -0.25, c = this.options, d = c.slicedOffset, e = d + c.borderWidth, f = c.center.concat([c.size, c.innerSize || 0]), g = this.chart, h = g.plotWidth, i = g.plotHeight,
k, j, m, n = this.points, p = 2 * ab, t, D = Za(h, i), x, r, q, w = c.dataLabels.distance, f = Xb(f, function (a, b) { return (x = /%$/.test(a)) ? [h, i, D, D][b] * Q(a) / 100 : a }); this.getX = function (a, b) { m = oa.asin((a - f[1]) / (f[2] / 2 + w)); return f[0] + (b ? -1 : 1) * fa(m) * (f[2] / 2 + w) }; this.center = f; o(n, function (b) { a += b.y }); o(n, function (c) {
    t = a ? c.y / a : 0; k = B(b * p * 1E3) / 1E3; b += t; j = B(b * p * 1E3) / 1E3; c.shapeType = "arc"; c.shapeArgs = { x: f[0], y: f[1], r: f[2] / 2, innerR: f[3] / 2, start: k, end: j }; m = (j + k) / 2; c.slicedTranslation = Xb([fa(m) * d + g.plotLeft, sa(m) * d + g.plotTop], B); r = fa(m) *
f[2] / 2; q = sa(m) * f[2] / 2; c.tooltipPos = [f[0] + r * 0.7, f[1] + q * 0.7]; c.labelPos = [f[0] + r + fa(m) * w, f[1] + q + sa(m) * w, f[0] + r + fa(m) * e, f[1] + q + sa(m) * e, f[0] + r, f[1] + q, w < 0 ? "center" : m < p / 4 ? "left" : "right", m]; c.percentage = t * 100; c.total = a
}); this.setTooltipPoints()
        }, render: function () { this.getAttribs(); this.drawPoints(); this.options.enableMouseTracking !== !1 && this.drawTracker(); this.drawDataLabels(); this.options.animation && this.animate && this.animate(); this.isDirty = !1 }, drawPoints: function () {
            var a = this.chart, b = a.renderer, c, d, e, f =
this.options.shadow, g, h; o(this.points, function (i) { d = i.graphic; h = i.shapeArgs; e = i.group; g = i.shadowGroup; if (f && !g) g = i.shadowGroup = b.g("shadow").attr({ zIndex: 4 }).add(); if (!e) e = i.group = b.g("point").attr({ zIndex: 5 }).add(); c = i.sliced ? i.slicedTranslation : [a.plotLeft, a.plotTop]; e.translate(c[0], c[1]); g && g.translate(c[0], c[1]); d ? d.animate(h) : i.graphic = b.arc(h).attr(N(i.pointAttr[Ga], { "stroke-linejoin": "round" })).add(i.group).shadow(f, g); i.visible === !1 && i.setVisible(!1) })
        }, drawDataLabels: function () {
            var a = this.data,
b, c = this.chart, d = this.options.dataLabels, e = p(d.connectorPadding, 10), f = p(d.connectorWidth, 1), g, h, i = p(d.softConnector, !0), k = d.distance, j = this.center, m = j[2] / 2, j = j[1], n = k > 0, u = [[], []], t, r, x, y, q = 2, w; if (d.enabled) {
                R.prototype.drawDataLabels.apply(this); o(a, function (a) { a.dataLabel && u[a.labelPos[7] < ab / 2 ? 0 : 1].push(a) }); u[1].reverse(); y = function (a, b) { return b.y - a.y }; for (a = u[0][0] && u[0][0].dataLabel && Q(u[0][0].dataLabel.styles.lineHeight); q--; ) {
                    var v = [], B = [], A = u[q], C = A.length, F; for (w = j - m - k; w <= j + m + k; w += a) v.push(w);
                    x = v.length; if (C > x) { h = [].concat(A); h.sort(y); for (w = C; w--; ) h[w].rank = w; for (w = C; w--; ) A[w].rank >= x && A.splice(w, 1); C = A.length } for (w = 0; w < C; w++) { b = A[w]; h = b.labelPos; b = 9999; for (r = 0; r < x; r++) g = ya(v[r] - h[1]), g < b && (b = g, F = r); if (F < w && v[w] !== null) F = w; else for (x < C - w + F && v[w] !== null && (F = x - C + w); v[F] === null; ) F++; B.push({ i: F, y: v[F] }); v[F] = null } B.sort(y); for (w = 0; w < C; w++) {
                        b = A[w]; h = b.labelPos; g = b.dataLabel; r = B.pop(); t = h[1]; x = b.visible === !1 ? $a : eb; F = r.i; r = r.y; if (t > r && v[F + 1] !== null || t < r && v[F - 1] !== null) r = t; t = this.getX(F === 0 ||
F === v.length - 1 ? t : r, q); g.attr({ visibility: x, align: h[6] })[g.moved ? "animate" : "attr"]({ x: t + d.x + ({ left: e, right: -e}[h[6]] || 0), y: r + d.y }); g.moved = !0; if (n && f) g = b.connector, h = i ? [ua, t + (h[6] === "left" ? 5 : -5), r, "C", t, r, 2 * h[2] - h[4], 2 * h[3] - h[5], h[2], h[3], ha, h[4], h[5]] : [ua, t + (h[6] === "left" ? 5 : -5), r, ha, h[2], h[3], ha, h[4], h[5]], g ? (g.animate({ d: h }), g.attr("visibility", x)) : b.connector = g = this.chart.renderer.path(h).attr({ "stroke-width": f, stroke: d.connectorColor || b.color || "#606060", visibility: x, zIndex: 3 }).translate(c.plotLeft,
c.plotTop).add()
                    } 
                } 
            } 
        }, drawTracker: xb.prototype.drawTracker, getSymbol: function () { } 
    }); Ca.pie = A; N(Highcharts, { Chart: vc, dateFormat: dc, pathAnim: Ia, getOptions: function () { return Ba }, hasBidiBug: Oc, numberFormat: fc, Point: Aa, Color: bb, Renderer: $b, SVGRenderer: Fb, VMLRenderer: Wa, CanVGRenderer: nc, seriesTypes: Ca, setOptions: function (a) { cc = M(cc, a.xAxis); kc = M(kc, a.yAxis); a.xAxis = a.yAxis = ba; Ba = M(Ba, a); uc(); return Ba }, Series: R, addEvent: ja, removeEvent: Na, createElement: xa, discardElement: Vb, css: U, each: o, extend: N, map: Xb, merge: M,
        pick: p, splat: Hb, extendClass: O, placeBox: tc, product: "Highcharts", version: "2.2.0"
    })
})();
